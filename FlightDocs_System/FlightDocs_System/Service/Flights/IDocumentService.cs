using FlightDocs_System.Data;
using FlightDocs_System.Model;

namespace FlightDocs_System.Service.Flights
{
    public interface IDocumentService
    {
       
        Task<ResponseModel> AddDocument(Document d);

    }
}
