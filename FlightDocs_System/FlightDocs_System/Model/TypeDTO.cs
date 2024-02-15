using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightDocs_System.Model
{
    public class TypeDTO
    {
       
        public string Id { get; set; }

        public string Name { get; set; }

        public string CreateAt { get; set; } 
        public string CreateBy { get; set; }

        public string UpdateAt { get; set; }

        public string UpdateBy { get; set; }
        public string? Note { get; set; }
        public bool isActive {  get; set; }
    }
}
