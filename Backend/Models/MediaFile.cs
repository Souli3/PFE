using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models
{
    public class MediaFile
    {
       [NotMapped]
       public IFormCollection ImageFile { get; set; }
        
        public int annonce_id { get; set; }
        
    }
}
