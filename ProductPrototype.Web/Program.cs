using ProductPrototype.DbAccess;
using ProductPrototype.DbAccess.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation(); //
// Example: Registering your GetDataFromDb class (if you inject it elsewhere)
builder.Services.AddScoped<GetDataFromDb>();

// Example: Registering the ProductService (if you inject it directly)
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<RepositoryContext>();

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
