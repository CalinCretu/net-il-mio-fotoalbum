using net_il_mio_fotoalbum.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Data
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(40, ErrorMessage = "Il campo puo' contenere massimo 40 caratteri")]
        public string Title { get; set; }
        [Required]
        [MinWords]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public bool IsVisible { get; set; }
        public int CategoryId { get; set; }

        public Photo() { }

        public Photo(string title, string description, string imageUrl, bool isVisible, int categoryId)

        {
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            IsVisible = isVisible;
            CategoryId = categoryId;
        }
    }
}
