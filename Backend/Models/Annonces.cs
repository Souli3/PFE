using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("annonces", Schema = "pfe")]
    public class Annonces
    {
        [Column("Annonce_id")]
        public int Id { get; set; }
        [Column("Titre")]
        public String Titre { get; set; }
        [Column("Description")]
        public String Description { get; set; }
        [Column("Prix")]
        public int Prix { get; set; }
        [Column("Etat")]
        public char Etat { get; set; }
        [Column("Genre")]
        public char Genre { get; set; }
        [Column("Vendeur_id")]
        public int Vendeur_id { get; set; }
        [Column("Categorie_id")]
        public int Categorie_id { get; set; }
        [Column("Acheteur_id")]
        public int Acheteur_id { get; set; }
    }
}
