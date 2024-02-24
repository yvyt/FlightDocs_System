using FlightDocs_System.Data;
using FlightDocs_System.Model;
using FlightDocs_System.Service.User;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System;
using FlightDocs_System.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection.Metadata;
using Document = FlightDocs_System.Data.Document;
using System.IO.Compression;

namespace FlightDocs_System.Service.Flights
{
    public class FlightDocumentService : IFlightDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFlightService _flightService;
        private readonly IDocumentService _documentService;
        private readonly ITypeDocumentService _typeDocumentService;
        public FlightDocumentService(ApplicationDbContext context, IUserService userService, IHttpContextAccessor httpContextAccessor, IFlightService flightService, ITypeDocumentService typeDocumentService, IDocumentService documentService)
        {
            _context = context;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _flightService = flightService;
            _typeDocumentService = typeDocumentService;
            _documentService = documentService;
        }

        public async Task<ResponseModel> AddFlightDocuments(FlightDocumentCreate fd)
        {
            var user = await UserFromAccessToken();
            if (user == null)
            {
                return new ResponseModel
                {
                    Message = "UnAuthorize",
                    IsSuccess = false,
                };
            }
            var type = await GetType(fd.Type);
            if (type == null)
            {
                return new ResponseModel
                {
                    Message = $"Not exist type with id={type}",
                    IsSuccess = false,
                };
            }
            var flight = await GetFlight(fd.Flight);
            if (flight == null)
            {
                return new ResponseModel
                {
                    Message = $"Not exist flight with id={flight}",
                    IsSuccess = false,
                };
            }
            string version = "1.0";
            var name = fd.FileContent.FileName;
            var path = $"Upload/{fd.Flight}/{name}/{version}/";

            var documentUpload = await _documentService.UploadFile(fd.FileContent, path);
            if (!documentUpload.IsSuccess)
            {
                return documentUpload;
            }
            Document data = (Document)documentUpload.Data;
            FlightDocument flightDocs = new FlightDocument
            {
                Name = name,
                TypeId = fd.Type,
                Version = version,
                FlightId = fd.Flight,
                Note = fd.Note,
                DocumentId = data.Id,
                CreateBy = user.Id,
                UpdateBy = user.Id,
            };
            await _context.FlightDocuments.AddAsync(flightDocs);
            int numberChange = await _context.SaveChangesAsync();
            if (numberChange <= 0)
            {
                return new ResponseModel
                {
                    Message = $"Error when add new flight document",
                    IsSuccess = false,
                };
            }
            return new ResponseModel
            {
                Message = $"Add new document successful",
                IsSuccess = true,
                Data = flightDocs
            };

        }
        private async Task<UserDTO> UserFromAccessToken()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var us = await _userService.GetUserByToken(accessToken);

            if (us == null)
            {
                return null;
            }
            return us;
        }
        private async Task<UserDTO> GetEmailUserFromId(string id)
        {
            var result = await _userService.GetById(id);
            if (result.IsSuccess)
            {
                return (UserDTO)result.Data;
            }
            return null;
        }
        private async Task<TypeDTO> GetType(string id)
        {
            var result = await _typeDocumentService.GetActiveById(id);
            if (result.IsSuccess)
            {
                return (TypeDTO)result.Data;

            }
            return null;
        }
        private async Task<FlightDTO> GetFlight(string id)
        {
            var result = await _flightService.GetActiveFlight(id);
            if (result.IsSuccess)
            {
                return (FlightDTO)result.Data;

            }
            return null;
        }

        public async Task<ResponseModel> GetAll()
        {
            var data = await _context.FlightDocuments.ToListAsync();
            List<FlightDocumentDTO> result = new List<FlightDocumentDTO>();
            foreach (var d in data)
            {
                var email = await GetEmailUserFromId(d.CreateBy);
                if (email == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not found user with id={d.CreateBy}",
                        IsSuccess = false
                    };
                }
                var typeDoc = await GetTypeDetail (d.TypeId);
                if (typeDoc == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not found type with id={d.TypeId}",
                        IsSuccess = false
                    };
                }

                var fl = await GetFlightDetail(d.FlightId);
                if (fl == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not found flight with id={d.FlightId}",
                        IsSuccess = false
                    };
                }
                FlightDocumentDTO doc = new FlightDocumentDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Type = typeDoc.Name,
                    Version = d.Version,
                    Flight = fl.FlightNo,
                    CreateAt = d.CreateAt,
                    CreateBy = email.Email,
                    UpdateAt = d.UpdateAt,
                    UpdateBy = email.Email,
                    IsActive = d.IsActive,
                    Note=d.Note
                };
                result.Add(doc);
            }
            return new ResponseModel
            {
                Message = "Get all successful",
                IsSuccess = true,
                Data = result
            };
        }

        private async Task<TypeDTO> GetTypeDetail(string typeId)
        {
            var result = await _typeDocumentService.GetById(typeId);
            if (result.IsSuccess)
            {
                return (TypeDTO)result.Data;

            }
            return null;
        }

        private async Task<FlightDTO> GetFlightDetail(string flightId)
        {
            var result = await _flightService.GetById(flightId);
            if (result.IsSuccess)
            {
                return (FlightDTO)result.Data;

            }
            return null;
        }

        public async Task<ResponseModel> GetById(string id)
        {
            var d = await _context.FlightDocuments.FirstOrDefaultAsync(x => x.Id == id);
            if (d == null)
            {
                return new ResponseModel
                {
                    Message = $"Not exist flight document with id={id}",
                    IsSuccess = false,
                };
            }
            var email = await GetEmailUserFromId(d.CreateBy);
            if (email == null)
            {
                return new ResponseModel
                {
                    Message = $"Not found user with id={d.CreateBy}",
                    IsSuccess = false
                };
            }
            var typeDoc = await GetTypeDetail(d.TypeId);
            if (typeDoc == null)
            {
                return new ResponseModel
                {
                    Message = $"Not found type with id={d.TypeId}",
                    IsSuccess = false
                };
            }

            var fl = await GetFlightDetail(d.FlightId);
            if (fl == null)
            {
                return new ResponseModel
                {
                    Message = $"Not found flight with id={d.FlightId}",
                    IsSuccess = false
                };
            }
            FlightDocumentDTO doc = new FlightDocumentDTO
            {
                Id = d.Id,
                Name = d.Name,
                Type = typeDoc.Name,
                Version = d.Version,
                Flight = fl.FlightNo,
                CreateAt = d.CreateAt,
                CreateBy = email.Email,
                UpdateAt = d.UpdateAt,
                UpdateBy = email.Email,
                IsActive = d.IsActive,
                Note=d.Note

            };
            return new ResponseModel
            {
                Message = "Get detail flight document successful",
                IsSuccess = true,
                Data = doc
            };
        }

        public async Task<ResponseModel> UpdateFlightDocument(string id, FlightDocumentUpdate fd)
        {
            try
            {
                var user = await UserFromAccessToken();
                if(user==null) {
                    return new ResponseModel
                    {
                        Message="UnAuthorize",
                        IsSuccess = false
                    };
                }
                var f = await _context.FlightDocuments.FirstOrDefaultAsync(x => x.Id == id);
                if (f == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not exist flight document with id={id}",
                        IsSuccess = false
                    };
                }
                
                var type = await GetType(fd.Type);
                if (type == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not exist type with id={type}",
                        IsSuccess = false,
                    };
                }
                var lastestVersion = await GetLatestVersion(f);
                var checkConfirm = await _context.FlightDocuments.FirstOrDefaultAsync(x => x.Name == f.Name && x.FlightId == f.FlightId && x.Version==lastestVersion);
                if (checkConfirm.IsConfirm){
                
                        return new ResponseModel
                        {
                            Message = "Documents have been confirmed, cannot be updated",
                            IsSuccess = false
                        };
                }
                var version = (float.Parse(lastestVersion) +1)*1.0/10;
                var updateVersion = version.ToString().Replace(',', '.');
                if (fd.FileContent != null)
                {
                    var name = fd.FileContent.FileName;
                    if (name != f.Name)
                    {
                        return new ResponseModel
                        {
                            Message = "File name is not same with older version",
                            IsSuccess = false,
                        };
                    }
                    var path = $"Upload/{f.FlightId}/{name}/{updateVersion}/";   
                    var uploadDoc = await _documentService.UploadFile(fd.FileContent, path);
                    if (!uploadDoc.IsSuccess)
                    {
                        return uploadDoc;
                    }
                    Document data = (Document)uploadDoc.Data;
                    FlightDocument flightDocs = new FlightDocument
                    {
                        Name = f.Name,
                        TypeId = fd.Type,
                        Version = updateVersion,
                        FlightId = f.FlightId,
                        Note = fd.Note,
                        DocumentId = data.Id,
                        CreateBy = user.Id,
                        UpdateBy = user.Id,
                    };
                    
                    await _context.FlightDocuments.AddAsync(flightDocs);
                    
                }
                f.TypeId = fd.Type;
                f.Note = fd.Note;
                f.UpdateAt = DateTime.Now;
                f.UpdateBy= user.Id;
                _context.Update(f);
                int numberChange = await _context.SaveChangesAsync();
                if (numberChange <= 0)
                {
                    return new ResponseModel
                    {
                        Message = $"Error when update flight document",
                        IsSuccess = false,
                    };
                }
                return new ResponseModel
                {
                    Message = $"Update  document successful",
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Message = $"Error when update flight document: {ex.Message}",
                    IsSuccess = false,
                };
            }
        }
        private async Task<string> GetLatestVersion(FlightDocument d)
        {
            var latestVersion = await _context.FlightDocuments
                .Where(x => x.Name == d.Name && x.FlightId == d.FlightId)
                .MaxAsync(x => x.Version);
            return latestVersion;
        }

        public async Task<ResponseModel> InActive(string id)
        {
            try
            {
                var user = await UserFromAccessToken();
                if (user == null)
                {
                    return new ResponseModel
                    {
                        Message = "UnAuthorize",
                        IsSuccess = false,
                    };
                }
                var fl = await _context.FlightDocuments.FirstOrDefaultAsync(x => x.Id == id);
                if (fl == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not exist flight document with id={id}",
                        IsSuccess = false
                    };
                }
                fl.IsActive = false;
                fl.UpdateAt = DateTime.Now;
                fl.UpdateBy = user.Id;
                _context.Update(fl);
                int numberChange = await _context.SaveChangesAsync();
                if (numberChange <= 0)
                {
                    return new ResponseModel
                    {
                        Message = "Error when inactive flight document",
                        IsSuccess = false,
                    };
                }
                return new ResponseModel
                {
                    Message = "Inactive Successful",
                    IsSuccess = true,
                    Data = fl
                };

            }
            catch (Exception ex)
            {
                
                return new ResponseModel
                {
                    Message = $"Error when inactive flight document: {ex.Message}",
                    IsSuccess = false,
                };
            }
        }

        public async Task<ResponseModel> GetByFlight(string id)
        {
            var fl = await GetFlightDetail(id);
            if (fl == null)
            {
                return new ResponseModel
                {
                    Message = $"Not found flight with id={id}",
                    IsSuccess = false
                };
            }
            var data = await _context.FlightDocuments.Where(x=>x.FlightId == id).ToListAsync();
            List<FlightDocumentDTO> result = new List<FlightDocumentDTO>();
            foreach (var d in data)
            {
                var email = await GetEmailUserFromId(d.CreateBy);
                if (email == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not found user with id={d.CreateBy}",
                        IsSuccess = false
                    };
                }
                var typeDoc = await GetTypeDetail(d.TypeId);
                if (typeDoc == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not found type with id={d.TypeId}",
                        IsSuccess = false
                    };
                }

                
                FlightDocumentDTO doc = new FlightDocumentDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Type = typeDoc.Name,
                    Version = d.Version,
                    Flight = fl.FlightNo,
                    CreateAt = d.CreateAt,
                    CreateBy = email.Email,
                    UpdateAt = d.UpdateAt,
                    UpdateBy = email.Email,
                    IsActive = d.IsActive,
                };
                result.Add(doc);
            }
            return new ResponseModel
            {
                Message = "Get all successful",
                IsSuccess = true,
                Data = result
            };
        }

        public async Task<(Stream, string)> DownloadFD(string id)
        {
            try
            {
                var file = await _context.FlightDocuments.FirstOrDefaultAsync(x => x.Id == id);
                if (file == null)
                {
                    return (null, $"Not exist flight document with id={id}");
                }
                var document = await _context.Documents.FirstOrDefaultAsync(x => x.Id == file.DocumentId);
                var fileName = document.Name;
                if (string.IsNullOrEmpty(fileName))
                {
                    return (null, $"File Name is Empty");
                }
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), document.Path);

                if (!File.Exists(filePath))
                {
                    return (null, $"File not found: {fileName}");
                }
                return (System.IO.File.OpenRead(filePath), document.Name);


            }
            catch (Exception ex)
            {
                return (null, $"Error '{ex.Message}' when download.");
            }
        }

        public async Task<ResponseModel> GetByType(string typeID)
        {
            var typeDoc = await GetTypeDetail(typeID);
            if (typeDoc == null)
            {
                return new ResponseModel
                {
                    Message = $"Not found type with id={typeID}",
                    IsSuccess = false
                };
            }
            var data = await _context.FlightDocuments.Where(x => x.TypeId == typeID).ToListAsync();
            List<FlightDocumentDTO> result = new List<FlightDocumentDTO>();
            foreach (var d in data)
            {
                var email = await GetEmailUserFromId(d.CreateBy);
                if (email == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not found user with id={d.CreateBy}",
                        IsSuccess = false
                    };
                }
               
                var fl = await GetFlightDetail(d.FlightId);
                if (fl == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not found flight with id={d.FlightId}",
                        IsSuccess = false
                    };
                }

                FlightDocumentDTO doc = new FlightDocumentDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Type = typeDoc.Name,
                    Version = d.Version,
                    Flight = fl.FlightNo,
                    CreateAt = d.CreateAt,
                    CreateBy = email.Email,
                    UpdateAt = d.UpdateAt,
                    UpdateBy = email.Email,
                    IsActive = d.IsActive,
                };
                result.Add(doc);
            }
            return new ResponseModel
            {
                Message = "Get all successful",
                IsSuccess = true,
                Data = result
            };
        }

        public async Task<byte[]> DownloadZip(string flightID)
        {
            try
            {
                var docs = await _context.FlightDocuments.Where(x => x.FlightId == flightID && x.IsConfirm).ToListAsync();
                if (docs == null)
                {
                    return null;
                }
                var filePaths = new List<string>();
                foreach (var doc in docs)
                {
                    var d = await _context.Documents.FirstOrDefaultAsync(x => x.Id == doc.DocumentId);
                    if (d == null)
                    {
                        return null;

                    }
                    filePaths.Add(d.Path);
                }
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var filePath in filePaths)
                        {
                            var fileInfo = new FileInfo(filePath);
                            if (fileInfo.Exists)
                            {
                                var zipArchiveEntry = archive.CreateEntry(fileInfo.Name, CompressionLevel.Fastest);
                                using (var zipStream = zipArchiveEntry.Open())
                                    await fileInfo.OpenRead().CopyToAsync(zipStream);
                            }
                        }
                    }
                    memoryStream.Position = 0;
                    return memoryStream.ToArray();

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseModel> ConfirmDocument(string id)
        {
            try
            {
                var user = await UserFromAccessToken();
                if (user == null)
                {
                    return new ResponseModel
                    {
                        Message = "UnAuthorize",
                        IsSuccess = false
                    };
                }
                var doc= await _context.FlightDocuments.FirstOrDefaultAsync(x=>x.Id == id);
                if (doc == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not exist flight document with id={id}",
                        IsSuccess = false
                    };
                }
                var lastest = await GetLatestVersion(doc);
                if (lastest.Equals(doc.Version))
                {
                    doc.IsConfirm = true;
                    doc.UpdateAt= DateTime.Now;
                    doc.UpdateBy = user.Id;
                    _context.FlightDocuments.Update(doc);
                    int numberChange = await _context.SaveChangesAsync();
                    if (numberChange <= 0)
                    {
                        return new ResponseModel
                        {
                            Message = $"Error when confirm flight document",
                            IsSuccess = false,
                        };
                    }
                    return new ResponseModel
                    {
                        Message = $"Confirm  document successful",
                        IsSuccess = true,
                    };
                }
                return new ResponseModel
                {
                    Message = "Only documents with the latest version are allowed to be confirmed",
                    IsSuccess = false,
                };
            }catch(Exception ex)
            {
                return new ResponseModel
                {
                    Message = $"Error when confirm",
                    IsSuccess = false
                };
            }
        }
    }
    
}
