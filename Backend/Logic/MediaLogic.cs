using Backend.Models;
using Backend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public interface IMediaLogic
    {
        Task AddMedias(List<string> photos, int annonce_id);
    }
    public class MediaLogic : IMediaLogic
    {
        private IMediaService _mediaService;
        public MediaLogic(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }
        public async Task AddMedias(List<string> photos, int annonce_id)
        {
            List<Media> media = new List<Media>(); 
            foreach(var photo in photos)
            {
                Media md = new Media()  ;
                md.Url = photo;
                md.Annonce_id=annonce_id;
                media.Add(md);
            }


            await _mediaService.AddMedia(media);
            return;
        }
    }
}
