using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using net_il_mio_fotoalbum.Models;

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
            using PhotoContext db = new PhotoContext();
            return db.Photos.ToList();
        }
        public static List<Category> GetAllCategories()
        {
            using PhotoContext db = new PhotoContext();
            return db.Categories.ToList();
        }
        public static Photo GetPhoto(int id, bool includeReferences = true)
        {
            using PhotoContext db = new PhotoContext();
            if (includeReferences)
                return db.Photos.Where(p => p.Id == id).Include(p => p.Categories).FirstOrDefault();
            return db.Photos.FirstOrDefault(p => p.Id == id);
        }

        public static Category GetCategoryById(int id)
        {
            using PhotoContext db = new PhotoContext();
            return db.Categories.FirstOrDefault(t => t.Id == id);
        }

        public static void InsertPhoto(Photo Photo, List<string> SelectedCategories = null)
        {
            using PhotoContext db = new PhotoContext();
            if (SelectedCategories != null)
            {
                Photo.Categories = new List<Category>();
                foreach (var categoryId in SelectedCategories)
                {
                    int id = int.Parse(categoryId);
                    var category = db.Categories.FirstOrDefault(t => t.Id == id);
                    Photo.Categories.Add(category);
                }
            }
            db.Photos.Add(Photo);
            db.SaveChanges();
        }

        public static bool UpdatePhoto(int id, Photo photo, List<string> selectedCategories)
        {
            using PhotoContext db = new PhotoContext();
            var photoDaModificare = db.Photos.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);
            if (photoDaModificare == null)
                return false;

            photoDaModificare.Title = photo.Title;
            photoDaModificare.Description = photo.Description;
            photoDaModificare.ImageUrl = photo.ImageUrl;
            photoDaModificare.IsVisible = photo.IsVisible;

            // Aggiorna le categorie
            photoDaModificare.Categories.Clear();
            var categories = db.Categories.Where(c => selectedCategories.Contains(c.Id.ToString())).ToList();
            photoDaModificare.Categories.AddRange(categories);

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                // Log dell'eccezione interna per ottenere maggiori dettagli
                Console.WriteLine(ex.InnerException?.Message);
                return false;
            }
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

        //public static void Seed()
        //{
        //    if (PhotoManager.CountPhotos() == 0)
        //    {
        //        PhotoManager.InsertPhoto(new Photo("Tramonto Estivo", "Foto di un tramonto d'estate", "url", true), Photo);
        //    }
        //}
    }
}

// var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
// MODO PER PRENDERE IL USER ID DELL'UTENTE LOGGATO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identityuser?view=aspnetcore-1.1&viewFallbackFrom=aspnetcore-6.0
//https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identityuserclaim-1?view=aspnetcore-1.1&viewFallbackFrom=aspnetcore-6.0