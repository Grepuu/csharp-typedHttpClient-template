using HttpClientPrototype.Web.Models;

namespace HttpClientPrototype.Web.HttpClients;

public interface ITodosHttpClient
{
    Task<ICollection<TodoDto>?> GetTodos();
    Task<TodoDto?> GetTodo(int id);
}