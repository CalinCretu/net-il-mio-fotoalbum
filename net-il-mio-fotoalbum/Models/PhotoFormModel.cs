using Microsoft.AspNetCore.Mvc.Rendering;
using net_il_mio_fotoalbum.Data;
using System.Collections.Generic;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {
        public Photo Photo { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public List<string>? SelectedCategories { get; set; }

        public PhotoFormModel()
        {
            Photo = new Photo();
            Categories = new List<SelectListItem>();
            SelectedCategories = new List<string>();
        }

        public PhotoFormModel(Photo photo)
        {
            Photo = photo ?? new Photo();
            SelectedCategories = new List<string>();
            if (Photo.Categories != null)
            {
                foreach (var i in Photo.Categories)
                    SelectedCategories.Add(i.Id.ToString());
            }
        }

        public void CreateCategories()
        {
            this.Categories = new List<SelectListItem>();
            if (this.SelectedCategories == null)
                this.SelectedCategories = new List<string>();
            var categoriesFromDb = PhotoManager.GetAllCategories();
            foreach (var category in categoriesFromDb)
            {
                bool isSelected = this.SelectedCategories.Contains(category.Id.ToString());
                this.Categories.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.Id.ToString(),
                    Selected = isSelected,
                });
            }
        }
    }
}
