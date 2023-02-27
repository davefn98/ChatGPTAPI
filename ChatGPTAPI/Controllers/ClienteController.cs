using Microsoft.AspNetCore.Mvc;

namespace ChatGPTAPI.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
