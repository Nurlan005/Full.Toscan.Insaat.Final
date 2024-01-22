using Toscan_Insaat_Final;
using Microsoft.EntityFrameworkCore;
using Toscan_Insaat_Final.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}"
         );
app.MapControllerRoute("default", "{controller=Home}/{action=Index}");
app.UseStaticFiles();

app.Run();
