using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Data
{
    public interface IDataContext
    {
        DbSet<Membre> Membres { get; set; }
        DbSet<Annonce> Annonces { get; set; }
        DbSet<Adresse> Adresses { get; set; }
        DbSet<Categorie> Categories { get; set; }
        DbSet<AnnonceAdresse> AnnonceAdresses { get; set; }
        DbSet<Media> Medias { get; set; }
        DbSet<Discussion> Discussions { get; set; }
        DbSet<Message> Messages { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
