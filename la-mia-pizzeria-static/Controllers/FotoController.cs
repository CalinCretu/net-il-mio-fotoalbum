using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Fotos = la_mia_pizzeria_static.Models.Fotos;
using Microsoft.AspNetCore.Authorization;

namespace la_mia_pizzeria_static.Controllers
{
    public class FotoController : Controller
    {
        private readonly ILogger<FotoController> _logger;

        public FotoController(ILogger<FotoController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            //FotoManager.ResetTable();
            return View(FotoManager.GetAllFotos());
        }

        public IActionResult VediFoto(int id)
        {
            var foto = FotoManager.VediFoto(id);
            if (foto != null)
            {
                return View(foto);
            }
            else
            {
                return View("errore");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            FotosFormModel model = new FotosFormModel();
            model.Fotos = new Fotos();
            model.CreateCategories();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(FotosFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.CreateCategories();

                return View("Create", data);
            }

            FotoManager.InsertFoto(data.Fotos, data.SelectedCategories);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateFoto(int id)
        {
            // Prendo il post AGGIORNATO da database, non
            // uno passato da utente alla action
            var fotoToEdit = FotoManager.VediFoto(id);

            if (fotoToEdit == null)
            {
                return NotFound();
            }
            else
            {
                FotosFormModel model = new FotosFormModel(fotoToEdit);
                model.CreateCategories();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateFoto(int id, FotosFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.CreateCategories();
                return View("UpdateFoto", data);
            }

            if (FotoManager.UpdateFoto(id, data.Fotos.Name, data.Fotos.Description, data.Fotos.Visible ,data.SelectedCategories))
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (FotoManager.DeleteFoto(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}