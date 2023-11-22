using bankAPI.Context;
using bankAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen();

// register Db conext wiht DI
builder.Services.AddDbContext<applicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("applicationDBConnect")));

// register repository pattern with DI
builder.Services.AddScoped<IRepository, CustomerRepository>();


//cross origin
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// cross origin
app.UseCors("corsapp");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
