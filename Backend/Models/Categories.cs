using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("categories", Schema = "pfe")]
    public class Categories
    {
        [Column("Categorie_id")]
        public int Id { get; set; }
        [Column("Nom")]
        public String Nom { get; set; }
        [Column("Sur_categorie_id")]
        public int Sur_categorie_id { get; set; }
    }
}
