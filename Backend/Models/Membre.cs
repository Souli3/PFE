using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models
{
    [Table("membres", Schema = "pfe")]
    public class Membre
    {
        [Column("membre_id")]
        public int Id { get; set; }
        [Column("email")]
        public String Email{ get; set; }
        [Column("mot_de_passe")]
        public string MotDePasse { get; set; }
        [Column("campus_id")]
        public int Campus_Id { get; set; }
        [Column("administrateur")]
        public bool Administrateur { get; set; }
        [Column("banni")]
        public DateTime? Banni { get; set; }
        [NotMapped]
        public Adresse Adresse { get; set; }
        [NotMapped]
        public String Token { get; set; }
    }
}
