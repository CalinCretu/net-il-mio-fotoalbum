using net_il_mio_fotoalbum.Data;

namespace net_il_mio_fotoalbum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //if (PhotoManager.CountPhotos() == 0)
            //{
            //    PhotoManager.InsertPhoto(new Photo("Tramonto Estivo", "Foto di un tramonto d'estate", "url", true, 1));
            //}
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Photo}/{action=Index}/{id?}");

            app.Run();
        }
    }
}