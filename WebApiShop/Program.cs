using WebApiShop.Controllers;
using Services;
using Repository;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped<IPasswordsController, PasswordsController>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<Ipasswordservice, passwordservice>();
builder.Services.AddScoped<IUserservice, Userservice>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
