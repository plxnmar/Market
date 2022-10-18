using Market.DAL;
using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.Service.Implementatins;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



// �������� ������ ����������� �� ����� ������������
string connection = builder.Configuration.GetConnectionString("DefaultConnection");


// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
//services.AddControllersWithViews();

//����������� ���������� IProductRepository � ������� �����������
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//����������� ������� ��������
builder.Services.AddScoped<IProductService, ProductService>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ��������� ������
//app.MapGet("/", (ApplicationDbContext db) => db.Products.ToList());


app.Run();
