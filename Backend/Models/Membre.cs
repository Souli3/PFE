using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("membres", Schema = "pfe")]
    public class Membre
    {
        [Column("membre_id")]
        public int Id { get; set; }
        [Column("email")]
        public String Email { get; set; }
        [Column("mot_de_passe")]
        public String MotDePasse { get; set; }
        [Column("campus_id")]
        public int Campus_Id { get; set; }
        [Column("administrateur")]
        public bool Administrateur { get; set; }
        [Column("banni")]
        public DateTime Banni { get; set; }
    }
}
