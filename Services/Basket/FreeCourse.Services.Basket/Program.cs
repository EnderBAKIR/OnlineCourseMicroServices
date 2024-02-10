using FreeCourse.Services.Basket.Services;
using FreeCourse.Services.Basket.Setting;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSetting>>().Value;

    var redis = new RedisService(redisSettings.Host , redisSettings.Port);

    redis.Connect();
    return redis;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//options pattern
builder.Services.Configure<RedisSetting>(builder.Configuration.GetSection("RedisSettings"));
//options pattern


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
