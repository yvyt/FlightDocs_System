using FlightDocs_System.Model;

namespace FlightDocs_System.Service.Flights
{
    public interface ITypeDocumentService
    {
        Task<ResponseModel> AddType(TypeCreate type);
        Task<ResponseModel> GetAll();
        Task<ResponseModel> GetById(string id);
        Task<ResponseModel> Inactive(string id);
        Task<ResponseModel> UpdateType(string id, TypeCreate type);
        Task<ResponseModel> GetActiveById(string id);
    }
}