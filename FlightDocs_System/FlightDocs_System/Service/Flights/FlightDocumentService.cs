using FlightDocs_System.Data;
using FlightDocs_System.Model;
using FlightDocs_System.Service.User;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs_System.Service.Flights
{
    public class FlightDocumentService:IFlightDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFlightService _flightService;
        private readonly IDocumentService _documentService;
        private readonly ITypeDocumentService _typeDocumentService;
        public FlightDocumentService(ApplicationDbContext context, IUserService userService, IHttpContextAccessor httpContextAccessor, IFlightService flightService, ITypeDocumentService typeDocumentService,IDocumentService documentService)
        {
            _context = context;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _flightService = flightService;
            _typeDocumentService = typeDocumentService;
            _documentService= documentService;
        }

        public async Task<ResponseModel> AddFlightDocuments(FlightDocumentCreate fd)
        {
              var user = await UserFromAccessToken();
                if(user == null)
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
                if(flight == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not exist flight with id={flight}",
                        IsSuccess = false,
                    };
                }
                var path = $"Upload/{fd.Flight}/{fd.Name}/{fd.Version}/";
                
                var documentUpload = await _documentService.UploadFile(fd.FileContent, path);
                if(!documentUpload.IsSuccess)
                {
                    return documentUpload;
                }
                Document data =(Document) documentUpload.Data;
                FlightDocument flightDocs = new FlightDocument
                {
                    Name = fd.Name,
                    TypeId = fd.Type,
                    Version = double.Parse(fd.Version),
                    FlightId = fd.Flight,
                    Note = fd.Note,
                    DocumentId = data.Id,
                    CreateBy=user.Id,
                    UpdateBy=user.Id,
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
                    Data=flightDocs
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
    }
}
