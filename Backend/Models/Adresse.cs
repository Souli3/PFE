using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("adresses", Schema = "pfe")]
    public class Adresse
    {
        [Column("adresse_id")]
        public int Id { get; set; }
        [Column("rue")]
        public String Rue { get; set; }
        [Column("numero")]
        public String Numero { get; set; }
        [Column("ville")]
        public String Ville { get; set; }
        [Column("code_postal")]
        public String Code_postal { get; set; }
        [Column("pays")]
        public String Pays { get; set; }
    }
}
