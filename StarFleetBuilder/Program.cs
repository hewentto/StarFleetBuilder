using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarFleetBuilder.Data;
using StarFleetBuilder.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StarFleetBuilderContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StarFleetBuilderContext") ?? throw new InvalidOperationException("Connection string 'StarFleetBuilderContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StarFleetBuilderContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("StarFleetBuilderContext")));
builder.Services.AddScoped<StarshipService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DatabaseSeeder>();

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedDatabaseAsync();
}

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
