using la_mia_pizzeria_static.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class FotosFormModel
    {
        public Fotos Fotos { get; set; }

        public List<SelectListItem>? Categories { get; set; }

        public List<string>? SelectedCategories { get; set; }

        public FotosFormModel() { } 
        public FotosFormModel(Fotos f)
        {
            this.Fotos = f;
        }


        public void CreateCategories()
        {
            this.Categories = new List<SelectListItem>();
            this.SelectedCategories = new List<string>();
            var categoriesFromDB = FotoManager.GetAllCategories();
            foreach (var category in categoriesFromDB)
            {
                bool isSelected = this.Fotos.Categories?.Any(i => i.Id == category.Id) == true;
                this.Categories.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.Id.ToString(),
                    Selected = isSelected,
                });
                if (isSelected)
                {
                    this.SelectedCategories.Add(category.Id.ToString());
                }
            }
        }
    }
}
