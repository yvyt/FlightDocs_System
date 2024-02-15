using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightDocs_System.Model
{
    public class TypeCreate
    {
       
        [Required]

        public string Name { get; set; }
        public string? Note { get; set; }
        
    }
}
