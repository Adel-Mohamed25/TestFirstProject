using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.BLL.Handlers.EmployeeHandlers;
using Project.BLL.Mapper;
using Project.BLL.Queries.EmployeeQueries;
using Project.BLL.Repository;
using Project.BLL.Services;
using Project.DAL.ConnectionData;
using Project.DAL.Entities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

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

// Add Automapper  
builder.Services.AddAutoMapper(mapper => mapper.AddProfile(new DomainProfile()));

//Add Cors => (Cross Origin Resource Shareing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:44349")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Add Scoped for appling DI
//builder.Services.AddScoped<IServicesRepo<Employee>, EmployeeRepo>();

builder.Services.AddScoped<IServicesRepo<Department>, DepartmentRepo>();

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
