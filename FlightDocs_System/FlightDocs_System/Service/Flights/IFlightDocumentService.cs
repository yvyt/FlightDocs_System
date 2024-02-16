using FlightDocs_System.Model;

namespace FlightDocs_System.Service.Flights
{
    public interface IFlightDocumentService
    {
        Task<ResponseModel> AddFlightDocuments(FlightDocumentCreate fd);
    }
}
