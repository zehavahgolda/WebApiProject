using AutoMapper;
using Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Repository;
using Services;
using WebApiShop.Controllers;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<IPasswordsController, PasswordsController>();
builder.Services.AddDbContext<Store_329391924Context>(options => options.UseSqlServer(
     builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IUserservice, UserService>();
builder.Services.AddScoped<ICatogeryRepsitory, CatogeryRepsitory>();
builder.Services.AddScoped<ICatgoryService,CatgoryService> ();
builder.Services.AddScoped<IOrderService,OrderService> ();
builder.Services.AddScoped<IOrderrRepository, OrderrRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductservice,Productservice  > ();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddOpenApi();

builder.Services.AddControllers();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Store_329391924Context>();
    context.Database.EnsureCreated();
}
//builder.Host.UseNLog();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
       options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
    });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
