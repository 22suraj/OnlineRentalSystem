using OnlineRentalSystemAPI;
using OnlineRentalSystemAPI.Data;
using OnlineRentalSystemAPI.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod(); ;
        });
});

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<RegisterService>();
builder.Services.AddSingleton<ProductService>();


var app = builder.Build();


app.MapGet("/", async (RegisterService registerService) => await registerService.Get());

app.MapGet("/api/products", async (ProductService productService) => {
    return await productService.Get(); 
});

app.MapPost("/api/createproduct", async (ProductService productService, Product product) =>
{
    await productService.Create(product);
    return Results.Ok("Product Successfully Added");
});

app.MapPost("/api/deleteproducts", async (ProductService productService, Product product) => {
    await productService.DeleteProduct(product);
    return Results.Ok("Product Successfully Deleted");
   
});

app.MapPost("/api/register", async (RegisterService registerService, RegisterUser user) =>
{
    return  Results.Ok(await registerService.Create(user));
});

app.MapPost("/api/login", async (RegisterService registerService, RegisterUser user)  =>{

    return Results.Ok(await registerService.Login(user));
});


app.UseCors();
app.Run();
