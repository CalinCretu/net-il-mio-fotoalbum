using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Data
{
    public class PhotoContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=MyPhotos;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
