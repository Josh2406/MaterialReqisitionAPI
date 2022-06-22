using MaterialRequisition.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Register DBContext
builder.Services.RegisterDbContext(builder.Configuration);

//Services Registration
builder.Services.RegisterAppServices();

//Swagger Registration
builder.Services.AddCustomSwaggerGen();

//Register Authentication
builder.Services.RegisterAuthentication();

//Settings
builder.Services.ConfigureApplicationSettings(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

//Configure Swagger
app.UseCustomSwaggerUI();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
//Use Swagger
app.UseCustomSwaggerUI();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
