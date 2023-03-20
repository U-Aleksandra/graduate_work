using Microsoft.EntityFrameworkCore;
using WebApiApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDatabaseContext>(o => o.UseSqlServer($"Data Source=DESKTOP-RI685Q8\\SQLEXPRESS;Initial Catalog={nameof(AppDatabaseContext)};Integrated Security=True; TrustServerCertificate=True")) ;
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
