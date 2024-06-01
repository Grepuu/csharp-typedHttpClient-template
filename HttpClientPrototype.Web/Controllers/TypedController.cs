using System.Diagnostics;
using HttpClientPrototype.Web.HttpClients._01_OldWay;
using HttpClientPrototype.Web.HttpClients._02_FromFactory;
using HttpClientPrototype.Web.HttpClients._03_Typed;
using Microsoft.AspNetCore.Mvc;
using HttpClientPrototype.Web.Models;

namespace HttpClientPrototype.Web.Controllers;

public class TypedController : Controller
{
    private readonly ILogger<TypedController> _logger;
    private readonly ApiServiceTypedService _typedService;

    public TypedController(
        ILogger<TypedController> logger,
        ApiServiceTypedService typedService
        )
    {
        _logger = logger;
        _typedService = typedService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _typedService.GetTodos();
        return Json(result);
    }

    public async Task<IActionResult> One(int id)
    {
        var result = await _typedService.GetTodo(id);
        return Json(result);
    }
}