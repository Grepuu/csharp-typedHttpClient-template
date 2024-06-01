using System.Diagnostics;
using HttpClientPrototype.Web.HttpClients._01_OldWay;
using Microsoft.AspNetCore.Mvc;
using HttpClientPrototype.Web.Models;

namespace HttpClientPrototype.Web.Controllers;

public class PrimitiveController : Controller
{
    private readonly ILogger<PrimitiveController> _logger;
    private readonly ApiServicePrimitiveService _primitiveService;

    public PrimitiveController(
        ILogger<PrimitiveController> logger,
        ApiServicePrimitiveService primitiveService
        )
    {
        _logger = logger;
        _primitiveService = primitiveService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _primitiveService.GetTodos();
        return Json(result);
    }

    public async Task<IActionResult> One(int id)
    {
        var result = await _primitiveService.GetTodo(id);
        return Json(result);
    }
}