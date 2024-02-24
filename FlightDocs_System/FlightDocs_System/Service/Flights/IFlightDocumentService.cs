using FlightDocs_System.Model;

namespace FlightDocs_System.Service.Flights
{
    public interface IFlightDocumentService
    {
        Task<ResponseModel> AddFlightDocuments(FlightDocumentCreate fd);
        Task<ResponseModel> GetAll();
        Task<ResponseModel> GetById(string id);
        Task<ResponseModel> InActive(string id);
        Task<ResponseModel> UpdateFlightDocument(string id, FlightDocumentUpdate fd);
        Task<ResponseModel> GetByFlight(string id);
        Task<(Stream, string)> DownloadFD(string id);
        Task<ResponseModel> GetByType(string typeID);
    }
}
