using HttpClientPrototype.Web.HttpClients._01_OldWay;
using HttpClientPrototype.Web.Models;
using Microsoft.Extensions.Options;

namespace HttpClientPrototype.Web.HttpClients._02_FromFactory;

public class ApiServiceFromFactoryService: ITodosHttpClient
{
    private readonly ApiServicePrimitiveOptions _apiSettings;
    private readonly IHttpClientFactory _factory;

    public ApiServiceFromFactoryService(
        IOptions<ApiServicePrimitiveOptions> apiSettings,
        IHttpClientFactory factory
    )
    {
        this._apiSettings = apiSettings.Value;
        this._factory = factory;
    }

    private HttpClient GenerateClient()
    {
        var client = _factory.CreateClient();

        switch (_apiSettings.AuthType)
        {
            case ApiSettingsAuthTypeEnum.Basic:
                var authCombined = $"{_apiSettings.Username}:{_apiSettings.Password}";
                var authEncrypted = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authCombined));
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {authEncrypted}");
                break;
            case ApiSettingsAuthTypeEnum.Token:
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiSettings.Token}");
                break;
            case ApiSettingsAuthTypeEnum.None:
                break;
            default:
                break;
        }
        
        client.DefaultRequestHeaders.Add("User-Agent", _apiSettings.AgentName);
        client.BaseAddress = new Uri(_apiSettings.BaseUrl);

        return client;
    }

    public async Task<ICollection<TodoDto>?> GetTodos()
    {
        var client = GenerateClient();
        
        var result = await client.GetFromJsonAsync<ICollection<TodoDto>>($"todos");
        
        // ensure positive result
        
        // log errors

        return result;
    }

    public async Task<TodoDto?> GetTodo(int id)
    {
        var client = GenerateClient();
        
        var result = await client.GetFromJsonAsync<TodoDto>($"todos/{id}");
        
        // ensure positive result
        
        // log errors

        return result;
    }
}