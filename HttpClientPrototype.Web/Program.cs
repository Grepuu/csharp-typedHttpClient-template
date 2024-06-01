using HttpClientPrototype.Web.HttpClients._01_OldWay;
using HttpClientPrototype.Web.HttpClients._02_FromFactory;
using HttpClientPrototype.Web.HttpClients._03_Typed;
using HttpClientPrototype.Web.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// ==================================================
// For Primitive api
// --------------------------------------------------

builder.Services.Configure<ApiServicePrimitiveOptions>(builder.Configuration.GetSection("ApiServicePrimitive"));
builder.Services.AddTransient<ApiServicePrimitiveService>();

// ==================================================
// For Factory api
// --------------------------------------------------

builder.Services.AddHttpClient();
builder.Services.Configure<ApiServiceFromFactoryOptions>(builder.Configuration.GetSection("ApiServiceFactory"));
builder.Services.AddTransient<ApiServiceFromFactoryService>();

// ==================================================
// For Typed client api
// --------------------------------------------------

builder.Services.Configure<ApiServiceTypedOptions>(builder.Configuration.GetSection("ApiServiceTyped"));
builder.Services.AddHttpClient<ApiServiceTypedService>((serviceProvider, httpClient) =>
{
    var apiOptions = serviceProvider
        .GetRequiredService<IOptions<ApiServiceTypedOptions>>()
        .Value;
    
    switch (apiOptions.AuthType)
    {
        case ApiSettingsAuthTypeEnum.Basic:
            var authCombined = $"{apiOptions.Username}:{apiOptions.Password}";
            var authEncrypted = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authCombined));
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {authEncrypted}");
            break;
        case ApiSettingsAuthTypeEnum.Token:
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiOptions.Token}");
            break;
        case ApiSettingsAuthTypeEnum.None:
            break;
        default:
            break;
    }

    httpClient.DefaultRequestHeaders.Add("User-Agent", apiOptions.AgentName);
    httpClient.BaseAddress = new Uri(apiOptions.BaseUrl);
});

// ==================================================


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();