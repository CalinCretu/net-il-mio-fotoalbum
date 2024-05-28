using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{       
    public class PhotoController : Controller
    {
        private readonly ILogger<PhotoController>? _logger;

        public PhotoController(ILogger<PhotoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(PhotoManager.GetAllPhotos());
        }

        [HttpGet]
        public IActionResult GetPhoto(int id)
        {
            var photo = PhotoManager.GetPhoto(id);
            return View(photo);
        }


        [HttpGet]
        public IActionResult CreatePhoto() // restituisce form creazione foto
        {
            Photo p = new Photo("Tramonto Estivo", "Foto di un tramonto d'estate", "https://picsum.photos/200", true);
            PhotoFormModel model = new PhotoFormModel(p);
            model.CreateCategories();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePhoto(PhotoFormModel photoDaInserire) 
        {
            if(ModelState.IsValid == false)
            {
                photoDaInserire.CreateCategories();
                // ritorno la form con i dati compilati dall'utente
                return View("CreatePhoto", photoDaInserire);
            }

            PhotoManager.InsertPhoto(photoDaInserire.Photo, photoDaInserire.SelectedCategories);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdatePhoto(int id) // restituisce form modifica foto
        {
            var photo = PhotoManager.GetPhoto(id);
            if (photo == null)
                return NotFound();

            var model = new PhotoFormModel(photo);
            model.CreateCategories();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePhoto(int id, PhotoFormModel photoFormModel, PhotoFormModel photoDaModificare)
        {
            if (!ModelState.IsValid)
            {
                photoFormModel.CreateCategories();
                return View(photoFormModel);
            }

            var photo = PhotoManager.GetPhoto(id);
            if (photo == null)
                return NotFound();

            photo.Title = photoFormModel.Photo.Title;
            photo.Description = photoFormModel.Photo.Description;
            photo.ImageUrl = photoFormModel.Photo.ImageUrl;
            photo.IsVisible = photoFormModel.Photo.IsVisible;
            photo.Categories = PhotoManager.GetAllCategories()
                .Where(c => photoFormModel.SelectedCategories.Contains(c.Id.ToString()))
                .ToList();

            var modified = PhotoManager.UpdatePhoto(id, photoDaModificare.Photo, photoDaModificare.SelectedCategories);
            if (modified)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePhoto(int id)
        {
          
            var deleted = PhotoManager.DeletePhoto(id);
            if (deleted)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
