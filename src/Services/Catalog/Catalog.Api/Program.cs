var builder = WebApplication.CreateBuilder(args);

//Add services to container
builder.Services.AddCarter();
builder.Services.AddMediatR(config=>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
}); //handle the business logic of the command handlers

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter(); //scan all codes to find ICarterModule interface and implement the mapped values

app.Run();
