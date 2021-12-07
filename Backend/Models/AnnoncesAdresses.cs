using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("annonces_adresses", Schema = "pfe")]
    public class AnnoncesAdresses
    {
        [Column("Annonce_adresse_id")]
        public int Annonces_adresse_id { get; set; }
        [Column("Annonce_id")]
        public int Annonce_id { get; set; }
        [Column("Adresse_id")]
        public int Adresse_id { get; set; }
    }
}
