using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAppfor_AW_worker.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebAppfor_AW_workerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppfor_AW_workerContext") ?? throw new InvalidOperationException("Connection string 'WebAppfor_AW_workerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();//Ading Session------------------------------------------------------------------

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

app.UseSession();//Ading Session------------------------------------------------------------------



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
