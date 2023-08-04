using Microsoft.AspNetCore.Mvc;

namespace WebAppfor_AW_worker.Areas.Worker.Controllers
{
    [Area("Worker")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
