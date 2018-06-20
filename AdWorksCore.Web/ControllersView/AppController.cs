using Microsoft.AspNetCore.Mvc;

namespace AdWorksCore.Web.ControllersView
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Info";
            return View();
        }
    }
}
