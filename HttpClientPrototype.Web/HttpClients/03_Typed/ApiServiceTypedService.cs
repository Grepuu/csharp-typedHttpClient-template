using HttpClientPrototype.Web.HttpClients._01_OldWay;
using HttpClientPrototype.Web.Models;
using Microsoft.Extensions.Options;

namespace HttpClientPrototype.Web.HttpClients._03_Typed;

public class  ApiServiceTypedService: ITodosHttpClient
{
    private readonly HttpClient _httpClient;

    public ApiServiceTypedService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public async Task<ICollection<TodoDto>?> GetTodos()
    {
        var result = await this._httpClient
            .GetFromJsonAsync<ICollection<TodoDto>>($"todos");
        
        // ensure positive result
        
        // log errors

        return result;
    }

    public async Task<TodoDto?> GetTodo(int id)
    {
        var result = await this._httpClient
            .GetFromJsonAsync<TodoDto>($"todos/{id}");
        
        // ensure positive result
        
        // log errors

        return result;
    }
}