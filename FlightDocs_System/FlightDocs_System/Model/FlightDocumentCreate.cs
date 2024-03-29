﻿using System.ComponentModel.DataAnnotations;

namespace FlightDocs_System.Model
{
    public class FlightDocumentCreate
    {
        [Required]
        public string Type { get; set; }
        
        [Required]
        public IFormFile FileContent { get; set; }

        [Required]
        public string Flight { get; set; }
        public string? Note { get; set; }
    }
}
