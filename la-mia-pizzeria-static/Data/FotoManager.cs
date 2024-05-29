using la_mia_pizzeria_static.Migrations;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Data

{
    public class FotoManager
    {
        public static int CountDbFotos()
        {
            using FotosContext db = new FotosContext();
            return db.Fotos.Count();
        }

        public static List<Fotos> GetAllFotos()
        {
            using FotosContext db = new FotosContext();
            return db.Fotos.ToList();
        }

        public static List<Category> GetAllCategories()
        {
            using FotosContext db = new FotosContext();
            return db.Categories.ToList();
        }

        public static void ResetTable()
        {
            using (var db = new FotosContext())
            {
                db.Fotos.RemoveRange(db.Fotos);
                db.SaveChanges();
            }
        }
        public static Fotos VediFoto(int id, bool includeReferences = true)
        {
            using FotosContext db = new FotosContext();
            if (includeReferences)
                return db.Fotos.Where(p => p.Id == id).Include(p => p.Categories).FirstOrDefault();
            return db.Fotos.FirstOrDefault(p => p.Id == id);
        }

        public static List<Fotos> GetFotoByName(string name)
        {
            using FotosContext db = new FotosContext();
            return db.Fotos.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public static Category GetCategoryById(int id)
        {
            using FotosContext db = new FotosContext();
            return db.Categories.FirstOrDefault(i => i.Id == id);
        }

        public static void InsertFoto(Fotos foto, List<string> SelectedCategories = null)
        {
            using FotosContext db = new FotosContext();
            if (SelectedCategories != null)
            {
                foto.Categories = new List<Category>();
                foreach (var categoryId in SelectedCategories)
                {
                    int id = int.Parse(categoryId);
                    var category = db.Categories.FirstOrDefault(t => t.Id == id);
                    foto.Categories.Add(category);
                }
            }
            db.Fotos.Add(foto);
            db.SaveChanges();
        }

        public static bool UpdateFoto(int id, string name, string description, bool visible, List<string> categories)
        {
            using FotosContext db = new FotosContext();
            var foto = db.Fotos.Where(x => x.Id == id).Include(x => x.Categories).FirstOrDefault();

            if (foto == null)
                return false;

            foto.Name = name;
            foto.Description = description;
            foto.Visible = visible;

            foto.Categories.Clear();
            if (categories != null)
            {
                foreach (var category in categories)
                {
                    int categoryId = int.Parse(category);
                    var categoryFromDb = db.Categories.FirstOrDefault(x => x.Id == categoryId);
                    foto.Categories.Add(categoryFromDb);
                }
            }

            db.SaveChanges();

            return true;
        }
        public static bool DeleteFoto(int id)
        {
            using FotosContext db = new FotosContext();
            var foto = db.Fotos.FirstOrDefault(p => p.Id == id);

            if (foto == null)
                return false;

            db.Fotos.Remove(foto);
            db.SaveChanges();

            return true;
        }

        public static void Seed()
        {
            if (FotoManager.CountDbFotos() == 0)
            {
                FotoManager.InsertFoto(new Fotos("Campo Fiorito", "Bellissimo campo di fiori", "~/img/CampoFiorito.jpg", true));
                FotoManager.InsertFoto(new Fotos("Lago di Montagna", "Freddissimo lago di montagna", "~/img/LagoGhiacciato.jpeg", true));
                FotoManager.InsertFoto(new Fotos("Lavanda <3", "Calmissimo tramonto", "~/img/Lavanda.jpg", true));
                FotoManager.InsertFoto(new Fotos("Montagnone", "Una Montagna Immensa", "~/img/Montagne.jpg", true));
            }
        }
    }
}