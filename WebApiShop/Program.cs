using Microsoft.EntityFrameworkCore;
using Repository;
using Entity;
using Services;
using WebApiShop.Controllers;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped<IPasswordsController, PasswordsController>();
builder.Services.AddDbContext<Store_329391924Context>(options => options.UseSqlServer(
    "Data Source=srv2\\pupils;Initial Catalog=Store_329391924;Integrated Security=True;Trust Server Certificate=True; Pooling=False"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<Ipasswordservice, PasswordService>();
builder.Services.AddScoped<IUserservice, Userservice>();
builder.Services.AddScoped<ICatogeryRepsitory, CatogeryRepsitory>();
builder.Services.AddScoped<ICatgoryService,CatgoryService> ();
builder.Services.AddScoped<IOrderService,OrderService> ();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductservice,Productservice  > ();
builder.Services.AddControllers();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(Options =>
    {
        Options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
   });
}







// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
