using FlightDocs_System.Model;

namespace FlightDocs_System.Service.Flights
{
    public interface IFlightService
    {
        Task<ResponseModel> CreateFlight(FlightCreate flight);
        Task<ResponseModel> GetAll();
        Task<ResponseModel> GetById(string id);
        Task<ResponseModel> InactiveFlight(string id);
        Task<ResponseModel> UpdateFlight(string id, FlightCreate flight);
        Task<ResponseModel> GetActiveFlight(string id);
    }
}
