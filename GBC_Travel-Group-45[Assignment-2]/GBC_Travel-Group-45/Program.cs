using GBC_Travel_Group_45.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Serilog.Events;
using Serilog;
using GBC_Travel_Group_45.Filters;

var builder = WebApplication.CreateBuilder(args);
// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/Serillogs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to true to require email confirmation for sign-in
})
.AddRoles<IdentityRole>()
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<ApplicationDbContext>();


//For Logging Filters 
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new LoggingFilterAttribute("details"));
});

var app = builder.Build();




if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithReExecute("/ErrorPage/{0}");
    app.UseExceptionHandler("/ErrorPage/500");
}
else
{
    app.UseStatusCodePagesWithReExecute("/ErrorPage/{0}");
    app.UseExceptionHandler("/ErrorPage/500");
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
