using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocs_System.Data
{
    [Table("Flight")]
    public class Flight
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]

        public string FlightNo { get; set; }
        [Required]

        public DateTime DateBegin { get; set; }
        [Required]

        public string From { get; set; }
        [Required]

        public string To { get; set; }
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

        public virtual ICollection<FlightDocument> FlightDocuments { get; set; }
    }
}
