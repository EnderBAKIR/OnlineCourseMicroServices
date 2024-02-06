using FreeCourse.Services.Catalog.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();

//Options Pattern MongoDb
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));//here we are binding the configuration to the DatabaseSettings class for the options pattern.

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;//here we are getting the value of the DatabaseSettings class from the options pattern.we use getRequiredService because we want to get the value of the DatabaseSettings class.
});
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
