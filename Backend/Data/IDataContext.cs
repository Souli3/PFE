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
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
