using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("discussions", Schema = "pfe")]
    public class Discussion
    {
        [Column("discussion_id")]
        public int Id { get; set; }
        [Column("membre_1")]
        public int Membre1_id { get; set; }
        [Column("membre_2")]
        public int Membre2_id { get; set; }
    }
}
