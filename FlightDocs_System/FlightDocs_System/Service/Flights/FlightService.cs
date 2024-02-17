using FlightDocs_System.Data;
using FlightDocs_System.Model;
using FlightDocs_System.Service.User;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs_System.Service.Flights
{
    public class FlightService : IFlightService
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
                Flight fl = new Flight
                {
                    FlightNo = flight.FlightNo,
                    DateBegin = flight.DateBegin,
                    From = flight.From,
                    To = flight.To,
                    CreateBy = user.Id,
                    UpdateBy = user.Id,
                };
                await _context.Flights.AddAsync(fl);
                int numberChange = await _context.SaveChangesAsync();
                if (numberChange <= 0)
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
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Message = $"Error: {ex.Message}",
                    IsSuccess = false,

                };
            }
        }

        public async Task<ResponseModel> GetAll()
        {
            var data = _context.Flights.ToList();
            List<FlightDTO> result = new List<FlightDTO>();
            foreach (var flight in data)
            {
                var userEmail = await GetEmailUserFromId(flight.CreateBy);
                if (userEmail == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not found user with id={flight.CreateBy}",
                        IsSuccess = false,
                    };
                }
                FlightDTO fl = new FlightDTO
                {
                    Id = flight.Id,
                    FlightNo = flight.FlightNo,
                    DateBegin = flight.DateBegin.ToString(),
                    From = flight.From,
                    To = flight.To,
                    CreateAt = flight.CreateAt.ToString(),
                    CreateBy = userEmail.Email,
                    UpdateAt = flight.UpdateAt.ToString(),
                    UpdateBy = userEmail.Email,
                    IsActive = flight.IsActive
                };
                result.Add(fl);
            }
            return new ResponseModel
            {
                Message = "Get all flight is successful",
                IsSuccess = true,
                Data = result
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
            if(result.IsSuccess)
            {
                return (UserDTO)result.Data;
            }
            return null;
        }

        public async Task<ResponseModel> GetById(string id)
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(x => x.Id == id);
            if (flight == null)
            {
                return new ResponseModel
                {
                    Message = $"Not exist flight with id={id}",
                    IsSuccess = false
                };
            }
            var userEmail = await GetEmailUserFromId(flight.CreateBy);
            if (userEmail == null)
            {
                return new ResponseModel
                {
                    Message = $"Not found user with id={flight.CreateBy}",
                    IsSuccess = false,
                };
            }
            FlightDTO fl = new FlightDTO
            {
                Id = flight.Id,
                FlightNo = flight.FlightNo,
                DateBegin = flight.DateBegin.ToString(),
                From = flight.From,
                To = flight.To,
                CreateAt = flight.CreateAt.ToString(),
                CreateBy = userEmail.Email,
                UpdateAt = flight.UpdateAt.ToString(),
                UpdateBy = userEmail.Email,
                IsActive = flight.IsActive
            };
            return new ResponseModel
            {
                Message = "Get detail flight successful",
                IsSuccess = true,
                Data = fl
            };
        }

        public async Task<ResponseModel> UpdateFlight(string id, FlightCreate flight)
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

                Flight fl = await _context.Flights.FirstOrDefaultAsync(x => x.Id == id);
                if (fl == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not exist flight with id={id}",
                        IsSuccess = false
                    };
                }
                fl.FlightNo=flight.FlightNo;
                fl.DateBegin = flight.DateBegin;
                fl.From = flight.From;
                fl.To=flight.To;
                fl.UpdateAt = DateTime.Now;
                fl.UpdateBy = user.Id;
                _context.Flights.Update(fl);
                int numberChange = await _context.SaveChangesAsync();
                if (numberChange <= 0)
                {
                    return new ResponseModel
                    {
                        Message = "Error when update flight",
                        IsSuccess = false,
                    };
                }
                return new ResponseModel
                {
                    Message = "Update flight successful",
                    IsSuccess = true,
                    Data = fl
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Message = $"Error: {ex.Message}",
                    IsSuccess = false,

                };
            }
        }

        public async Task<ResponseModel> InactiveFlight(string id)
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

                Flight fl = await _context.Flights.FirstOrDefaultAsync(x => x.Id == id);
                if (fl == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not exist flight with id={id}",
                        IsSuccess = false
                    };
                }
                fl.IsActive = false;
                fl.UpdateAt = DateTime.Now;
                fl.UpdateBy = user.Id;
                _context.Flights.Update(fl);
                int numberChange = await _context.SaveChangesAsync();
                if (numberChange <= 0)
                {
                    return new ResponseModel
                    {
                        Message = "Error when inactive flight",
                        IsSuccess = false,
                    };
                }
                return new ResponseModel
                {
                    Message = "Inactive flight successful",
                    IsSuccess = true,
                    Data = fl
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Message = $"Error: {ex.Message}",
                    IsSuccess = false,

                };
            }
        }

        public async Task<ResponseModel> GetActiveFlight(string id)
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(x => x.Id == id && x.IsActive==true);
            if (flight == null)
            {
                return new ResponseModel
                {
                    Message = $"Not exist flight with id={id}",
                    IsSuccess = false
                };
            }
            var userEmail = await GetEmailUserFromId(flight.CreateBy);
            FlightDTO fl = new FlightDTO
            {
                Id = flight.Id,
                FlightNo = flight.FlightNo,
                DateBegin = flight.DateBegin.ToString(),
                From = flight.From,
                To = flight.To,
                CreateAt = flight.CreateAt.ToString(),
                CreateBy = userEmail.Email,
                UpdateAt = flight.UpdateAt.ToString(),
                UpdateBy = userEmail.Email,
                IsActive = flight.IsActive
            };
            return new ResponseModel
            {
                Message = "Get detail flight successful",
                IsSuccess = true,
                Data = fl
            };
        }
    }
}
