using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.





builder.Services.AddControllers();

builder.Services.AddScoped<ICategoryService , CategoryService>();
builder.Services.AddScoped<ICourseService , CourseService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//Options Pattern MongoDb
//here we are binding the configuration to the DatabaseSettings class for the options pattern.

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;//here we are getting the value of the DatabaseSettings class from the options pattern.we use getRequiredService because we want to get the value of the DatabaseSettings class.
});
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));




//Options Pattern

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
