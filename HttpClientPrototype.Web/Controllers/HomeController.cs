using System.Diagnostics;
using HttpClientPrototype.Web.HttpClients._01_OldWay;
using Microsoft.AspNetCore.Mvc;
using HttpClientPrototype.Web.Models;

namespace HttpClientPrototype.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApiServicePrimitiveService _primitiveService;

    public HomeController(
        ILogger<HomeController> logger,
        ApiServicePrimitiveService primitiveService
        )
    {
        _logger = logger;
        _primitiveService = primitiveService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GetTodosFromPrimitive()
    {
        var result = await _primitiveService.GetTodos();
        return Json(result);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}