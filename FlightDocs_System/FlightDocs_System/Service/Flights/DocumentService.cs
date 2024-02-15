using FlightDocs_System.Data;
using FlightDocs_System.Model;

namespace FlightDocs_System.Service.Flights
{
    public class DocumentService:IDocumentService
    {
        private readonly ApplicationDbContext _context;

        public DocumentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<ResponseModel> AddDocument(Document d)
        {
            throw new NotImplementedException();
        }
    }
}
