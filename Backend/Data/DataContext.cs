using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Membre> Membres { get; set; }
        public DbSet<Annonce> Annonces { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Categorie> Categories { get ; set ; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<AnnonceAdresse> AnnonceAdresses { get ; set ; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
