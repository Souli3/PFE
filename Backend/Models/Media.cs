using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("medias", Schema = "pfe")]
    public class Media
    {
        [Column("media_id")]
        public int Id { get; set; }
        [Column("url")]
        public String Url { get; set; }
        [Column("annonce_id")]
        public int Annonce_id { get; set; }
    }
}
