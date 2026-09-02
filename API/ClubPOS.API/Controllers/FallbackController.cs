using Microsoft.AspNetCore.Mvc;

namespace ClubPOS.API.Controllers;

public class FallbackController : Controller
{
    public IActionResult Index()
    {
        return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot", "index.html"), "text/HTML");
    }
}