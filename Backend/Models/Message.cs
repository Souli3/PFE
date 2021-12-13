using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("discussions", Schema = "pfe")]
    public class Message
    {
        [Column("message_id")]
        public int Id { get; set; }
        [Column("discussion_id")]
        public int Discussion_id { get; set; }
        [Column("envoyeur")]
        public int Envoyeur_id { get; set; }
        [Column("texte")]
        public String Texte { get; set; }
        [Column("date_envoi")]
        public DateTime Date_envoi { get; set; }
    }
}
