//using AssignmentFirst.Services.Implementation;
//using AssignmentFirst.Services.Interfaces;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped<IProductService, ProductService>();

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


//app.UseHttpsRedirection();
//app.UseRouting();

//app.UseAuthorization();

//app.MapStaticAssets();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();


//app.Run();
using Microsoft.EntityFrameworkCore;
using AssignmentFirst.Services.Interfaces;
using AssignmentFirst.Services;
using AssignmentFirst.DbLayer;
using AssignmentFirst.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlite("Data Source=inventory.db"));

// Register custom services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();

