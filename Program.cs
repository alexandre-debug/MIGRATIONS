using Microsoft.EntityFrameworkCore;
using CatalogoProdutos.Data;
using CatalogoProdutos.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CatalogoProdutosContext>(
    options => options.UseMySql(
        builder.Configuration.GetConnectionString("ConnectDb"),

        ServerVersion.AutoDetect( builder.Configuration.GetConnectionString("ConnectDb"))
    )
);

builder.Services.AddScoped<UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//Desabilitando o CORS para o endereco do Front
//Coloque o endereco do seu front
app.UseCors(policy =>
    policy.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader()
);

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
