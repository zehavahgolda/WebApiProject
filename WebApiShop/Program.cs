using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Services;
using Repository;
using Entity;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", policy =>
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()
              .AllowAnyMethod());
});


builder.Services.AddDbContext<Store_329391924Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryRepository, CatogeryRepsitory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserservice, UserService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductservice, Productservice>();
builder.Services.AddScoped<ICatgoryService, CatgoryService>();
builder.Services.AddScoped<IOrderrRepository, OrderrRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "My API V1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseCors("CorsPolicy");

app.UseAuthorization();
app.MapControllers();

app.Run(); 