using Microsoft.EntityFrameworkCore;
using MFalcone_WEBAPI.Models;
using MFalcone_WEBAPI.Services.SolicitudServices;
using MFalcone_WEBAPI.Services.UsuarioServices;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") 
                   .AllowAnyMethod()                     
                   .AllowAnyHeader();                    
        });
});
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ISolicitudService, SolicitudService>();



builder.Services.AddDbContext<M_Falcone_BDContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
