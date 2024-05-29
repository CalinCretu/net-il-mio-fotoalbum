using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("foto")]
    [Index(nameof(Id), IsUnique = true)]
    public class Fotos
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatoria")]
        [MinLength(5, ErrorMessage = "La descrizione deve contenere almeno 5 caratteri")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Il campo immagine deve essere un URL valido")]
        public string? Image { get; set; }

        public List<Category>? Categories { get; set; }

        // New property
        public bool Visible { get; set; } = true;

        public Fotos(string name, string description, string image, bool visible)
        {
            Name = name;
            Description = description;
            Image = image;
            Visible = visible; // Default value for new objects
        }

        public Fotos() { }
    }
}
