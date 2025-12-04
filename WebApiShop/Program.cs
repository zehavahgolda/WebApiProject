using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Models;
using Services;
using WebApiShop.Controllers;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped<IPasswordsController, PasswordsController>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<Ipasswordservice, passwordservice>();
builder.Services.AddScoped<IUserservice, Userservice>();
builder.Services.AddDbContext<Store_329391924Context> (options => options.UseSqlServer(
    "Data Source=srv2\\pupils;Initial Catalog=Store_329391924;Integrated Security=True;Pooling=False"));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
