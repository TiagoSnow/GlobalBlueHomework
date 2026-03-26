using GlobalBlueHomework.Features.Endpoints;
using GlobalBlueHomework.Features.Services;
using GlobalBlueHomework.Features.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPurchaseValuesValidator, PurchaseValuesValidator>();
builder.Services.AddScoped<IPurchaseValuesService, PurchaseValuesService>();

var app = builder.Build();

app.AddPurchaseValueEndpoints();

app.Run();

public partial class Program { }