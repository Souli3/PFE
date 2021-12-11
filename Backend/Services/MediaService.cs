using Backend.Data;
using Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IMediaService
    {
        Task AddMedia(List<Media> media);
        Task<List<Annonce>> GetAllMediaFromListAnnonce(List<Annonce> annonces);
        Task<Annonce> GetAllMediaFromAnnonce(Annonce annonce);
    }
    public class MediaService : IMediaService
    {
        private IDataContext _dataContext;
        public MediaService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task AddMedia(List<Media> media)
        {
            foreach(Media mediaItem in media)
            {
                _dataContext.Medias.Add(mediaItem); 
                await _dataContext.SaveChangesAsync();

            }
            
        }
        public async Task<List<Annonce>> GetAllMediaFromListAnnonce(List<Annonce> annonces)
        {
            


            foreach (Annonce annonce in annonces)
            {
                annonce.UrlPhoto = new List<string>();
                annonce.UrlPhoto = _dataContext.Medias.Where(x=>x.Annonce_id==annonce.Id).Select(x=>x.Url).ToList();
            }
            return annonces;
        }
        public async Task<Annonce> GetAllMediaFromAnnonce(Annonce annonce)
        {
            annonce.UrlPhoto = new List<string>();
            
            annonce.UrlPhoto = _dataContext.Medias.Where(x => x.Annonce_id == annonce.Id).Select(x => x.Url).ToList();
            return annonce;
        }
    }
}
