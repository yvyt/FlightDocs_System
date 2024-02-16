using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocs_System.Data
{
    [Table("Document")]
    public class Document
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string ContentType { get; set; }
        [Required]

        public string Path { get; set; }

        public FlightDocument FlightDocument { get; set; }
    }
}
