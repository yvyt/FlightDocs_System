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
                var typeDoc = await GetType(d.TypeId);
                if (typeDoc == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not found type with id={d.TypeId}",
                        IsSuccess = false
                    };
                }

                var fl = await GetFlight(d.FlightId);
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
            var typeDoc = await GetType(d.TypeId);
            if (typeDoc == null)
            {
                return new ResponseModel
                {
                    Message = $"Not found type with id={d.TypeId}",
                    IsSuccess = false
                };
            }

            var fl = await GetFlight(d.FlightId);
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
                IsActive = d.IsActive
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
                .MaxAsync(x => x.Version); // Assuming x.Version is a string

            return latestVersion; // latestVersion is already a string or null
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
           
        
    }
}
