using FlightDocs_System.Model;

namespace FlightDocs_System.Service.Flights
{
    public interface IFlightService
    {
        Task<ResponseModel> CreateFlight(FlightCreate flight);
    }
}
