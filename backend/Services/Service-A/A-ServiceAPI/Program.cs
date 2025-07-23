using A_ServiceAPI.Context;
using A_ServiceAPI.Service;
using ConfigurationReaderAPI.Service;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<CaseContext>(options =>
{
    var configuration = builder.Configuration;
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddHostedService(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();

    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new ConfigurationReader(
        "SERVICE-A",
        connectionString,
        30000 
    );
});

var app = builder.Build();
app.MapGet("/", () => "A-Service çalýþýyor!");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
