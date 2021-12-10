using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("annonces_adresses", Schema = "pfe")]
    public class AnnonceAdresse
    {
        [Column("annonce_adresse_id")]
        public int Id { get; set; }
        [Column("annonce_id")]
        public int Annonce_id { get; set; }
        [Column("adresse_id")]
        public int Adresse_id { get; set; }
    }
}
