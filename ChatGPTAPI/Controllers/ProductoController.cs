using Microsoft.AspNetCore.Mvc;

namespace ChatGPTAPI.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
