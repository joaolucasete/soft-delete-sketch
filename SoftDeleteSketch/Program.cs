using Microsoft.EntityFrameworkCore;
using RT.Comb.AspNetCore;
using SoftDeleteSketch;
using SoftDeleteSketch.Entities;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(
    options => options
        .UseNpgsql("Host=localhost;Database=SoftDeleteSketch;Username=postgres;Password=Senha!01")
);
builder.Services.AddPostgreSqlCombGuidWithSqlDateTime();
builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapDefaultControllerRoute();

app.UseHttpsRedirection();

app.Run();