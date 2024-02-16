using FlightDocs_System.Data;
using FlightDocs_System.Model;
using FlightDocs_System.Service.User;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs_System.Service.Flights
{
    public class TypeDocumentService : ITypeDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TypeDocumentService(ApplicationDbContext context, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel> AddType(TypeCreate type)
        {
            try
            {
                var user = await UserFromAccessToken();
                if (user == null)
                {
                    return new ResponseModel
                    {
                        Message = "UnAuthorzie",
                        IsSuccess = false
                    };
                }
                TypeDocument d = new TypeDocument
                {
                    Name = type.Name,
                    Note = type.Note,
                    CreateBy = user.Id,
                    UpdateBy = user.Id,
                };
                await _context.AddAsync(d);
                int numberChanges = await _context.SaveChangesAsync();
                if (numberChanges <= 0)
                {
                    return new ResponseModel
                    {
                        Message = $"Error when create type",
                        IsSuccess = false,
                    };
                }
                return new ResponseModel
                {
                    Message = "Create new type successful",
                    IsSuccess = true,
                    Data = d
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Message = $"Error when create type: {ex.Message}",
                    IsSuccess = false,
                };
            }
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
            return (UserDTO)result.Data;
        }

        public async Task<ResponseModel> GetAll()
        {
            var data = await _context.TypeDocument.ToListAsync();
            List<TypeDTO> result = new List<TypeDTO>();
            foreach (var type in data)
            {
                var email = await GetEmailUserFromId(type.CreateBy);
                TypeDTO dto = new TypeDTO
                {
                    Id = type.Id,
                    Name = type.Name,
                    Note = type.Note,
                    CreateAt = type.CreateAt.ToString(),
                    CreateBy = email.Email.ToString(),
                    UpdateAt = type.UpdateAt.ToString(),
                    UpdateBy = email.Email.ToString(),
                    isActive = type.isActive,
                };
                result.Add(dto);
            }
            return new ResponseModel
            {
                Message = "Get all type successful",
                IsSuccess = true,
                Data = result
            };
        }

        public async Task<ResponseModel> GetById(string id)
        {
            var type = await _context.TypeDocument.FirstOrDefaultAsync(x => x.Id == id);
            if (type == null)
            {
                return new ResponseModel
                {
                    Message = $"Not exist type with id={id}",
                    IsSuccess = false,
                };
            }
            var email = await GetEmailUserFromId(type.CreateBy);
            TypeDTO dto = new TypeDTO
            {
                Id = type.Id,
                Name = type.Name,
                Note = type.Note,
                CreateAt = type.CreateAt.ToString(),
                CreateBy = email.Email.ToString(),
                UpdateAt = type.UpdateAt.ToString(),
                UpdateBy = email.Email.ToString(),
                isActive = type.isActive,
            };
            return new ResponseModel
            {
                Message = "Get detail type successful",
                IsSuccess = true,
                Data = dto
            };
        }

        public async Task<ResponseModel> UpdateType(string id, TypeCreate type)
        {
            try
            {
                var user = await UserFromAccessToken();
                if (user == null)
                {
                    return new ResponseModel
                    {
                        Message = "UnAuthorzie",
                        IsSuccess = false
                    };
                }
                var t = await _context.TypeDocument.FirstOrDefaultAsync(x => x.Id == id);
                if (t == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not exist type with id={id}",
                        IsSuccess = false,
                    };
                }

                t.Name = type.Name;
                t.Note = type.Note;
                t.UpdateAt = DateTime.Now;
                t.UpdateBy = user.Id;
                
                _context.Update(t);
                int numberChanges = await _context.SaveChangesAsync();
                if (numberChanges <= 0)
                {
                    return new ResponseModel
                    {
                        Message = $"Error when update type",
                        IsSuccess = false,
                    };
                }
                return new ResponseModel
                {
                    Message = "Update  type successful",
                    IsSuccess = true,
                    Data = t
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Message = $"Error when update type: {ex.Message}",
                    IsSuccess = false,
                };
            }
        }

        public async Task<ResponseModel> Inactive(string id)
        {
            try
            {
                var user = await UserFromAccessToken();
                if (user == null)
                {
                    return new ResponseModel
                    {
                        Message = "UnAuthorzie",
                        IsSuccess = false
                    };
                }
                var t = await _context.TypeDocument.FirstOrDefaultAsync(x => x.Id == id);
                if (t == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not exist type with id={id}",
                        IsSuccess = false,
                    };
                }

                t.isActive = false;
                t.UpdateAt = DateTime.Now;
                t.UpdateBy = user.Id;

                _context.Update(t);
                int numberChanges = await _context.SaveChangesAsync();
                if (numberChanges <= 0)
                {
                    return new ResponseModel
                    {
                        Message = $"Error when inactive type",
                        IsSuccess = false,
                    };
                }
                return new ResponseModel
                {
                    Message = "Inactive  type successful",
                    IsSuccess = true,
                    Data = t
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Message = $"Error when inactive type: {ex.Message}",
                    IsSuccess = false,
                };
            }
        }

        public async Task<ResponseModel> GetActiveById(string id)
        {
            var type = await _context.TypeDocument.FirstOrDefaultAsync(x => x.Id == id && x.isActive==true);
            if (type == null)
            {
                return new ResponseModel
                {
                    Message = $"Not active type with id={id}",
                    IsSuccess = false,
                };
            }
            var email = await GetEmailUserFromId(type.CreateBy);
            TypeDTO dto = new TypeDTO
            {
                Id = type.Id,
                Name = type.Name,
                Note = type.Note,
                CreateAt = type.CreateAt.ToString(),
                CreateBy = email.Email.ToString(),
                UpdateAt = type.UpdateAt.ToString(),
                UpdateBy = email.Email.ToString(),
                isActive = type.isActive,
            };
            return new ResponseModel
            {
                Message = "Get detail type successful",
                IsSuccess = true,
                Data = dto
            };
        }
    }
}
