using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("annonces", Schema = "pfe")]
    public class Annonce
    {
        [Column("annonce_id")]
        public int Id { get; set; }
        [Column("titre")]
        public String Titre { get; set; }
        [Column("description")]
        public String Description { get; set; }
        [Column("prix")]
        public int Prix { get; set; }
        [Column("etat")]
        public char Etat { get; set; }
        [Column("genre")]
        public char Genre { get; set; }
        [Column("vendeur_id")]
        public int Vendeur_id { get; set; }
        [Column("categorie_id")]
        public int Categorie_id { get; set; }
        [Column("acheteur_id")]
        public int Acheteur_id { get; set; }
    }
}
