using System.Security.Claims;

namespace net_il_mio_fotoalbum.Data
{
    public static class PhotoManager
    {
        public static int CountPhotos()
        {
            PhotoContext db = new PhotoContext();
            return db.Photos.Count();
        }
        public static List<Photo> GetAllPhotos()
        {
            PhotoContext db = new PhotoContext();
            return db.Photos.ToList();
        }
        public static Photo GetPhoto(int id)
        {
            PhotoContext db = new PhotoContext();
            return db.Photos.FirstOrDefault(p => p.Id == id);
        }

        public static void InsertPhoto(Photo photo)
        {
            using PhotoContext db = new PhotoContext();
            db.Photos.Add(photo);
            db.SaveChanges();
        }

        public static bool UpdatePhoto(int id, Photo photo)
        {
            using PhotoContext db = new PhotoContext();
            var photoDaModificare = db.Photos.FirstOrDefault(p =>p.Id == id);
            if (photoDaModificare == null)
                return false;
            photoDaModificare.Title = photo.Title;
            photoDaModificare.Description = photo.Description;
            photoDaModificare.ImageUrl = photo.ImageUrl;
            photoDaModificare.IsVisible = photo.IsVisible;
            photoDaModificare.CategoryId = photo.CategoryId;

            
            db.SaveChanges();
            return true;
        }

        public static bool DeletePhoto(int id)
        {
            using PhotoContext db = new PhotoContext();
            var photoDaCancellare = db.Photos.FirstOrDefault(p => p.Id == id);
            if (photoDaCancellare == null)
                return false;

            db.Remove(photoDaCancellare);
            db.SaveChanges();
            return true;

        }

        public static void Seed()
        {
            if (PhotoManager.CountPhotos() == 0)
            {
                PhotoManager.InsertPhoto(new Photo("Tramonto Estivo", "Foto di un tramonto d'estate", "url", true, 1));
            }
        }
    }
}

// var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
// MODO PER PRENDERE IL USER ID DELL'UTENTE LOGGATO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identityuser?view=aspnetcore-1.1&viewFallbackFrom=aspnetcore-6.0
//https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identityuserclaim-1?view=aspnetcore-1.1&viewFallbackFrom=aspnetcore-6.0