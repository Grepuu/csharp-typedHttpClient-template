using System.Diagnostics;
using HttpClientPrototype.Web.HttpClients._01_OldWay;
using HttpClientPrototype.Web.HttpClients._02_FromFactory;
using Microsoft.AspNetCore.Mvc;
using HttpClientPrototype.Web.Models;

namespace HttpClientPrototype.Web.Controllers;

public class FactoryController : Controller
{
    private readonly ILogger<FactoryController> _logger;
    private readonly ApiServiceFromFactoryService _factoryService;

    public FactoryController(
        ILogger<FactoryController> logger,
        ApiServiceFromFactoryService factoryService
        )
    {
        _logger = logger;
        _factoryService = factoryService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _factoryService.GetTodos();
        return Json(result);
    }

    public async Task<IActionResult> One(int id)
    {
        var result = await _factoryService.GetTodo(id);
        return Json(result);
    }
}