using Api.Entities;
using Api.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var name = builder.Configuration.GetValue<string>("Named");
var ichi = builder.Configuration.GetValue(typeof(string), "Info:Study:Kursdoshlar[2]");
Console.WriteLine(name);
Console.WriteLine(ichi);
var n = builder.Configuration.GetSection("Info").Get<Info>();
builder.Services.Configure<Info>(builder.Configuration.GetSection("Info"));
builder.Services.AddCors(cors => 
{
    cors.AddDefaultPolicy(corsP =>
    {
        corsP.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var logger = new LoggerConfiguration()
    .WriteTo.File("logchi.txt", Serilog.Events.LogEventLevel.Error)
        .WriteTo.Console().CreateLogger();

builder.Logging.AddSerilog(logger);
// builder.Services.AddScoped<Logger>();
// builder.Logging.AddProvider(new LoggerProvider());
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();
app.UseCors();
// {
    // cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
// });
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.Run();
