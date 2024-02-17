using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightDocs_System.Model
{
    public class FlightDocumentDTO
    {
      
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Version { get; set; } 
        public string Flight { get; set; }
        public string Note { get; set; }
        public DateTime CreateAt { get; set; } 
        
        public string CreateBy { get; set; }


        public DateTime UpdateAt { get; set; } 

        public string UpdateBy { get; set; }

        public bool IsActive { get; set; } 
    }
}
