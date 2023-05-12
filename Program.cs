using Api.Entities;
using Api.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var logger = new LoggerConfiguration()
    .WriteTo.File("logchi.txt", Serilog.Events.LogEventLevel.Error)
        .WriteTo.Console().CreateLogger();

builder.Logging.AddSerilog(logger);
// builder.Services.AddScoped<Logger>();
// builder.Logging.AddProvider(new LoggerProvider());
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.Run();
