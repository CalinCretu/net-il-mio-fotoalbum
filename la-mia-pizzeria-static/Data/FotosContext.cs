using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data
{
    public class FotosContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Fotos> Fotos { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=InitialPizzasDB;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}