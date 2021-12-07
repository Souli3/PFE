using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("medias", Schema = "pfe")]
    public class Medias
    {
        [Column("Media_id")]
        public int Id { get; set; }
        [Column("Url")]
        public String Url { get; set; }
        [Column("Annonce_id")]
        public int Annonce_id { get; set; }
    }
}
