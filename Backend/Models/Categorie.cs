using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("categories", Schema = "pfe")]
    public class Categorie
    {
        [Column("categorie_id")]
        public int Id { get; set; }
        [Column("nom")]
        public String Nom { get; set; }
        [Column("sur_categorie_id")]
        public int Sur_categorie_id { get; set; }
    }
}
