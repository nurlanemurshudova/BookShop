using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Context;

namespace BookShopWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); 
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddScoped<IAuthorDal, AuthorDal>();
            builder.Services.AddScoped<IAuthorService, AuthorManager>();

            builder.Services.AddScoped<IBasketItemDal, BasketItemDal>();
            builder.Services.AddScoped<IBasketItemService, BasketItemManager>();

            builder.Services.AddScoped<IBasketDal, BasketDal>();
            builder.Services.AddScoped<IBasketService, BasketManager>();

            builder.Services.AddScoped<IBookAuthorDal, BookAuthorDal>();
            builder.Services.AddScoped<IBookAuthorService, BookAuthorManager>();

            builder.Services.AddScoped<IBookDal, BookDal>();
            builder.Services.AddScoped<IBookService, BookManager>();

            builder.Services.AddScoped<IUserDal, UserDal>();
            builder.Services.AddScoped<IUserService, UserManager>();


            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            
            app.UseSession();

            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}
