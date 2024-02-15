using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocs_System.Data
{
    [Table("TypeDocument")]

    public class TypeDocument
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public DateTime CreateAt { get; set; } = DateTime.Now;
        [Required]

        public string CreateBy { get; set; }
        [Required]

        public DateTime UpdateAt { get; set; } = DateTime.Now;
        [Required]

        public string UpdateBy { get; set; }
        [StringLength (255)]
        public string? Note {  get; set; }
        [Required]
        public bool isActive { get; set; } = true;
        public virtual ICollection<FlightDocument> FlightDocuments { get; set; }

    }
}
