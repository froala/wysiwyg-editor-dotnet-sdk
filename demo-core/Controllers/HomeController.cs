using Microsoft.AspNetCore.Mvc;

namespace demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {   
            FroalaEditor.File file = new FroalaEditor.File(HttpContext);
            file.Upload("uploads/");
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
