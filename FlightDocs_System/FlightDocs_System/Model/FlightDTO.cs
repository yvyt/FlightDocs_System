using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightDocs_System.Model
{
    public class FlightDTO
    {
      
        public string Id { get; set; }
        public string FlightNo { get; set; }


        public string DateBegin { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string CreateAt { get; set; }

        public string CreateBy { get; set; }

        public string UpdateAt { get; set; } 
       

        public string UpdateBy { get; set; }
        public bool IsActive { get; set; }

    }
}
