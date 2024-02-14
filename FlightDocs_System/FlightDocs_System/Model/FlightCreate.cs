using System.ComponentModel.DataAnnotations;

namespace FlightDocs_System.Model
{
    public class FlightCreate
    {
        [Required]

        public string FlightNo { get; set; }
        [Required]

        public DateTime DateBegin { get; set; }
        [Required]

        public string From { get; set; }
        [Required]

        public string To { get; set; }
    }
}
