using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Asp_mvc.Data;
var builder = WebApplication.CreateBuilder(args);

//Configuração do dbContext banco - EF
builder.Services.AddDbContext<AspContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AspContext") ?? throw new InvalidOperationException("Connection string 'AspContext' not found.")));

// Add services to the container.
// Injeção de dependência
builder.Services.AddTransient<IFilmeRepository, FilmeRepository>();

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
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
