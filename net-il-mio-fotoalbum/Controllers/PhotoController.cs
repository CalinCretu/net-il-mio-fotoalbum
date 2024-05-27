using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class PhotoController : Controller
    {
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
            Photo p = new Photo("Tramonto Estivo", "Foto di un tramonto d'estate", "url", true);
            PhotoFormModel model = new PhotoFormModel();
            model.CreateCategories();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePhoto(Photo photoDaInserire) 
        {
            if(ModelState.IsValid == false)
            {
                // ritorno la form con i dati compilati dall'utente
                return View("CreatePhoto", photoDaInserire);
            }

            PhotoManager.InsertPhoto(photoDaInserire);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdatePhoto(int id) // restituisce form modifica foto
        {
            var photo = PhotoManager.GetPhoto(id);
            if (photo == null)
                return NotFound();
            return View(photo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePhoto(int id, Photo photoDaModificare)
        {
            if (ModelState.IsValid == false)
            {
                // ritorno la form con i dati compilati dall'utente
                return View("UpdatePhoto", photoDaModificare);
            }
            var modified = PhotoManager.UpdatePhoto(id, photoDaModificare);
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
