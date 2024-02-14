using FlightDocs_System.Data;
using FlightDocs_System.Model;
using FlightDocs_System.Service.User;

namespace FlightDocs_System.Service.Flights
{
    public class FlightService:IFlightService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FlightService(ApplicationDbContext context, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel> CreateFlight(FlightCreate flight)
        {
            var user = await UserFromAccessToken();
            if(user == null)
            {
                return new ResponseModel
                {
                    Message = "UnAuthorzie",
                    IsSuccess = false
                };
            }
            Flight fl = new Flight
            {
                FlightNo=flight.FlightNo,
                DateBegin=flight.DateBegin,
                From=flight.From,
                To=flight.To,
                CreateBy=user.Id,
                UpdateBy=user.Id,
            };
            await _context.Flights.AddAsync(fl);
            int numberChange = await _context.SaveChangesAsync();
            if(numberChange <= 0)
            {
                return new ResponseModel
                {
                    Message = "Error when create new flight",
                    IsSuccess = false,
                };
            }
            return new ResponseModel
            {
                Message = "Create new flight successful",
                IsSuccess = true,
                Data = fl
            };
        }

        private async Task<UserDTO> UserFromAccessToken()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var us= await _userService.GetUserByToken(accessToken);

            if (us == null)
            {
                return null;
            }
            return us;
        }
    }
}
