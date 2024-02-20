using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocs_System.Data
{
    [Table("FlightDocument")]

    public class FlightDocument
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string Name {  get; set; }
        [Required]
        public string TypeId { get; set; }
        [Required]
        public string Version { get; set; } = "1.0";
        [Required]
        public string FlightId { get; set; }
        public string? Note { get; set; }
        [Required]
        public string DocumentId {  get; set; }
        [Required]

        public DateTime CreateAt { get; set; } = DateTime.Now;
        [Required]

        public string CreateBy { get; set; }
        [Required]

        public DateTime UpdateAt { get; set; } = DateTime.Now;
        [Required]

        public string UpdateBy { get; set; }
        [Required]

        public bool IsActive { get; set; } = true;

        public Flight Flight { get; set; }
        public TypeDocument TypeDocument { get; set; }
        public virtual Document Document { get; set; }

    }
}
