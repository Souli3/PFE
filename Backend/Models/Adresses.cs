using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("adresses", Schema = "pfe")]
    public class Adresses
    {
        [Column("Adresse_id")]
        public int Id { get; set; }
        [Column("Rue")]
        public String Rue { get; set; }
        [Column("Numero")]
        public String Numero { get; set; }
        [Column("Ville")]
        public String Ville { get; set; }
        [Column("Code_postal")]
        public String Code_postal { get; set; }
        [Column("Pays")]
        public String Pays { get; set; }
    }
}
