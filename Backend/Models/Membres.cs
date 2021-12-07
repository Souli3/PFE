using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("membres", Schema = "pfe")]
    public class Membres
    {
        [Column("Membre_id")]
        public int Id { get; set; }
        [Column("Email")]
        public String Email { get; set; }
        [Column("Mot_de_passe")]
        public String Mot_de_passe { get; set; }
        [Column("Campus_id")]
        public Adresses Campus_id { get; set; }
        [Column("Administrateur")]
        public bool Administrateur { get; set; }
        [Column("Banni")]
        public DateTime Banni { get; set; }
    }
}
