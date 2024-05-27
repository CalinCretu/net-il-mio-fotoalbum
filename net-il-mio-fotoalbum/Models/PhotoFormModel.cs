using Microsoft.AspNetCore.Mvc.Rendering;
using net_il_mio_fotoalbum.Data;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {

        public Photo Photo { get; set; }

        public List<SelectListItem>? Categories { get; set; }

        public List<string>? SelectedCategories { get; set; }

        public PhotoFormModel() { }

        public PhotoFormModel(Photo photo)
        {
            Photo = photo;
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
                //    if (isSelected)
                //    {
                //        this.SelectedCategories.Add(category.Id.ToString());
                //    }
                //}
            }
        }
    }
}
