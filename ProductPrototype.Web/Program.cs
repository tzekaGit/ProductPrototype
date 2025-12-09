using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductPrototype.DbAccess;
using ProductPrototype.DbAccess.Services;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<RepositoryContext>(options =>
//{
//    // <-- Enable the setting inside the options builder lambda
//    options.EnableSensitiveDataLogging();
//});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation(); //
// Example: Registering your GetDataFromDb class (if you inject it elsewhere)
builder.Services.AddScoped<GetDataFromDb>();
builder.Services.AddScoped<HtmlProductExtractor>();


// Example: Registering the ProductService (if you inject it directly)
builder.Services.AddScoped<ProductService>();
// Use AddDbContext, which is specific for DbContext instances
builder.Services.AddDbContext<RepositoryContext>(options =>
{
	// This is where you configure the database provider (SQL Server, MySQL, etc.)
	// and pass the connection string, typically read from configuration.
	options.UseSqlServer(builder.Configuration.GetConnectionString("Belson4.91"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets()
   ;

app.Run();
