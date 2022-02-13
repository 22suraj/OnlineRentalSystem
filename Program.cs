using OnlineRentalSystemAPI;
using OnlineRentalSystemAPI.Data;
using OnlineRentalSystemAPI.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<RegisterService>();
builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

app.MapGet("/", async (RegisterService registerService) => await registerService.Get());

app.MapGet("/api/products", async (ProductService productService) => await productService.Get());

app.MapPost("/api/createproduct", async (ProductService productService, Product product) =>
{
    await productService.Create(product);
    return Results.Ok("Product Successfully Added");
});

app.MapPost("/api/register", async (RegisterService registerService, RegisterUser user) =>
{
    await registerService.Create(user);
    return Results.Ok("User Successfully Registered");
});

app.MapPost("/api/login", async (RegisterService registerService, RegisterUser user)  =>{

    return await registerService.Login(user);
});



app.Run();
