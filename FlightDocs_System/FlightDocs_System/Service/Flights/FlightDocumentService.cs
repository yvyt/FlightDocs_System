using FlightDocs_System.Data;
using FlightDocs_System.Service.User;

namespace FlightDocs_System.Service.Flights
{
    public class FlightDocumentService:IFlightDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFlightService _flightService;

        public FlightDocumentService(ApplicationDbContext context, IUserService userService, IHttpContextAccessor httpContextAccessor, IFlightService flightService)
        {
            _context = context;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _flightService = flightService;
        }
    }
}
