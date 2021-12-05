using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("personnes", Schema = "projet")]
    public class Personnes
    {
        [Column("id_personne")]
        public int Id { get; set; }
        [Column("nom")]
        public String Name { get; set; }

    }
}
