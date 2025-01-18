using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.BLL.Caching;
using Project.BLL.Handlers.EmployeeHandlers;
using Project.BLL.Mapper;
using Project.BLL.Queries.EmployeeQueries;
using Project.BLL.Repository;
using Project.BLL.Services;
using Project.DAL.ConnectionData;
using Project.DAL.Extend;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add SeriLog
var logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

Log.Logger = logger;

try
{
    Log.Information("Application Starting ....");
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //[1] add update Settings Json to solve (Circular Reference)
    //builder.Services.AddControllers().AddJsonOptions(options =>
    //{
    //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //    options.JsonSerializerOptions.WriteIndented = true;
    //});

    //[2] add update Settings Json to solve (Circular Reference)
    //builder.Services.AddControllers()
    //    .AddNewtonsoftJson(options =>
    //    {
    //        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    //    });


    // Add connection string 
    var connectionstring = builder.Configuration.GetConnectionString("ApplicationConnection");

    // Add dbcontext
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionstring));

    // Add Distributed Caching With Redis
    builder.Services.AddStackExchangeRedisCache(option =>
    {
        option.Configuration = builder.Configuration.GetConnectionString("Redis");
        option.InstanceName = "EntityInstance";
    });

    //Add Cors => (Cross Origin Resource Shareing)
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin", builder =>
        {
            builder.WithOrigins("https://localhost:44349/")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
    });

    // Add Identity for User and Role.
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 8;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.AllowedForNewUsers = true;

    }).AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders();


    // Add Authentication for User.

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:IssuerUrl"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:AudienceUrl"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]))
        };
    });

    // Add Automapper  
    builder.Services.AddAutoMapper(mapper => mapper.AddProfile(new DomainProfile()));

    // Add Logging
    builder.Services.AddLogging();

    // Add Scoped for appling DI
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),
        typeof(GetAllEmployeesQuery).Assembly,
        typeof(GetAllEmployeesQueryHandler).Assembly
    ));



    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowSpecificOrigin");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, ex.Message);
}
finally
{
    Log.CloseAndFlush();
}
