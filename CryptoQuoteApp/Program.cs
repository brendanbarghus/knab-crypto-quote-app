using CryptoQuoteApp.Services;
using CryptoQuoteApp.Services.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("CoinMarketCap", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CoinMarketCap:BaseUrl"]);
    client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", builder.Configuration["CoinMarketCap:ApiKey"]);
});

builder.Services.AddHttpClient("ExchangeRates", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ExchangeRates:BaseUrl"]);
});

builder.Services.AddScoped<CoinMarketCapService>();
builder.Services.AddScoped<ExchangeRatesService>();
builder.Services.AddScoped<ICryptoHelperService, CryptoHelperService>();

// Register Swagger generator with version info
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Crypto Quote API",
        Version = "v1",
        Description = "A simple API to manage cryptocurrency quotes",
    });

    // Optionally, include XML comments if you want to generate documentation from XML comments
    var xmlFile = Path.Combine(AppContext.BaseDirectory, "CryptoQuoteApp.xml");
    if (File.Exists(xmlFile))
    {
        c.IncludeXmlComments(xmlFile);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger();

// Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crypto Quote API V1");
    c.RoutePrefix = "swagger";  // Makes Swagger UI available at the root URL
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();