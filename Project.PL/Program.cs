using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.BLL.Mapper;
using Project.BLL.Repository;
using Project.BLL.Services;
using Project.DAL.ConnectionData;
using Project.DAL.Entities;
using Project.DAL.Extend;
using Project.PL.Languages;
using System.Globalization;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddControllersWithViews()
//        .AddNewtonsoftJson(options =>
//            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});



// Resources
builder.Services.AddMvc()
       .AddDataAnnotationsLocalization(options =>
       {
           options.DataAnnotationLocalizerProvider = (type, factory) =>
               factory.Create(typeof(SharedResource));
       });

// Add connection string 
var connectionstring = builder.Configuration.GetConnectionString("ApplicationConnection");

// Add dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionstring));


// Add Automapper  
builder.Services.AddAutoMapper(mapper => mapper.AddProfile(new DomainProfile()));


// Add Scoped for appling DI
builder.Services.AddScoped<IServicesRepo<Department>, DepartmentRepo>();

builder.Services.AddScoped<IServicesRepo<Employee>, EmployeeRepo>();

builder.Services.AddScoped<EmployeeRepo>();

builder.Services.AddScoped<IDistrictRepo, DistrictRepo>();

builder.Services.AddScoped<ICityRepo, CityRepo>();

builder.Services.AddScoped<ICountryRepo, CountryRepo>();

builder.Services.Configure<RequestLocalizationOptions>(option =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG"),
    };

    option.DefaultRequestCulture = new RequestCulture("en-US");
    option.SupportedCultures = supportedCultures;
    option.SupportedUICultures = supportedCultures;
    option.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
});



// Identity Configuration

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        options.LoginPath = new PathString("/User/SignIn");
        options.AccessDeniedPath = new PathString("/User/SignIn");
    });


// Add Confirmed Account
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);



// Password and user name configuration

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Default Password settings.
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<ApplicationDbContext>();


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

var localizationoptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationoptions);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");

app.Run();
