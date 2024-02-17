using System.ComponentModel.DataAnnotations;

namespace FlightDocs_System.Model
{
    public class FlightDocumentUpdate
    {
        [Required]
        public string Type { get; set; }

        public IFormFile? FileContent { get; set; }
        public string? Note { get; set; }
    }
}
