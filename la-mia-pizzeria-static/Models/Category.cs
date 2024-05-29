using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Fotos> Fotos { get; set; }

        public Category() { }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Category( string name)
        {
            Name = name;
        }
    }
}
