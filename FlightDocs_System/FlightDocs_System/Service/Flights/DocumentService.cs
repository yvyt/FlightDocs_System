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

        public async Task<ResponseModel> AddDocument(DocumentCreate d)
        {
            try
            {
                var doc = new Document
                {
                    Name = d.FileName,
                    ContentType = Path.GetExtension(d.FileName),
                    Path = d.path
                };
                await _context.Documents.AddAsync(doc);
                int numberChange = await _context.SaveChangesAsync();
                if (numberChange <= 0)
                {
                    return new ResponseModel
                    {
                        Message = "Error when add new document",
                        IsSuccess = false,
                    };
                }
                return new ResponseModel
                {
                    Message = "Add new document is successful",
                    IsSuccess = true,
                    Data= d
                };
            }
            catch(Exception ex)
            {
                return new ResponseModel
                {
                    Message = $"Error when add new document: {ex.Message}",
                    IsSuccess = false,
                };
            }
        }

        public async Task<ResponseModel> UploadFile(IFormFile file, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            var document = new Document
            {
                Name = file.FileName,
                ContentType = Path.GetExtension(file.FileName)
            };

            var uniqueFileName = document.Name;
            var filePath = Path.Combine(path, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                document.Path = filePath;
            }
            await _context.Documents.AddAsync(document);
            int number = await _context.SaveChangesAsync();
            if (number <= 0)
            {
                return new ResponseModel
                {
                    Message = $"Error when upload file",
                    IsSuccess = false,
                };
            }

            return new ResponseModel
            {
                Message = "Upload successful",
                IsSuccess = true,
                Data = document
            };
        }
    }
}
