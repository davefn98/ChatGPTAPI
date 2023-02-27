using Microsoft.AspNetCore.Mvc;

namespace ChatGPTAPI.Controllers
{
    public class VentaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
