﻿using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IAnnonceServices
    {
        Task<List<Annonce>> GetAllAnnoncesAsync();
        Task<List<Annonce>> GetAnnoncesByIdVendeur(int id);
        Task<Annonce> AddAnnonce(Annonce annonce);
        Annonce GetAnnonceById(int id);
        Task<Annonce> UpdateAnnonce(Annonce annonce);
        Task<List<Annonce>> GetAllAnnoncesByCategorie(Categorie categorie);
        Task<List<Annonce>> GetAnnoncesByIdAdresse(int idAdresse);
    }
    public class AnnonceService : IAnnonceServices
    {
        private IDataContext _dataContext;
        public AnnonceService (IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Annonce> AddAnnonce(Annonce annonce)
        {
            Annonce newAnnonce = _dataContext.Annonces.Add(annonce).Entity;
            await _dataContext.SaveChangesAsync();
            return newAnnonce;
        }

        public async Task<List<Annonce>> GetAllAnnoncesAsync()
        {
            return await _dataContext.Annonces.OrderBy(annonce => annonce.Id).ToListAsync();
        }

        public async Task<List<Annonce>> GetAllAnnoncesByCategorie(Categorie categorie)
        {
            return await _dataContext.Annonces.Where(annonce => annonce.Categorie_id == categorie.Id).OrderBy(annonce => annonce.Id).ToListAsync();
        }

        public Annonce GetAnnonceById(int id)
        {
            return _dataContext.Annonces.Where(annonce => annonce.Id == id).FirstOrDefault();
        }

        public async Task<List<Annonce>> GetAnnoncesByIdAdresse(int idAdresse)
        {
            return await _dataContext.Annonces
                .FromSqlRaw("SELECT a.* FROM pfe.annonces a, pfe.annonces_adresses aa WHERE a.Annonce_id = aa.Annonce_id AND aa.Adresse_id = {0}", idAdresse)
                .ToListAsync();
        }

        public async Task<List<Annonce>> GetAnnoncesByIdVendeur(int id)
        {
            return await _dataContext.Annonces.Where(annonce => annonce.Vendeur_id == id).ToListAsync();
        }

        public async Task<Annonce> UpdateAnnonce(Annonce annonce)
        {
            Annonce annonceDB = _dataContext.Annonces.Where(a => a.Id == annonce.Id).FirstOrDefault();
            annonceDB.Titre = annonce.Titre;
            annonceDB.Description = annonce.Description;
            annonceDB.Prix = annonce.Prix;
            annonceDB.Etat = annonce.Etat;
            await _dataContext.SaveChangesAsync();
            return annonceDB;
        }
    }
}
