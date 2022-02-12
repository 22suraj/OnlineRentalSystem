using OnlineRentalSystemAPI;
using OnlineRentalSystemAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<RegisterService>();

var app = builder.Build();

app.MapGet("/", async (RegisterService registerService) => await registerService.Get());

app.MapPost("/api/register", async (RegisterService registerService, RegisterUser user) =>
{
    await registerService.Create(user);
    return Results.Ok("User Successfully Registered");
});

app.MapPost("/api/login", async (RegisterService registerService, RegisterUser user)  =>{

    return await registerService.Login(user);
});



app.Run();
