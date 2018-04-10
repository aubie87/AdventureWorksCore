using AdWorksCore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWorksCore.Web.ControllersView
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //throw new InvalidOperationException("Bad thing just happened!");
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if(ModelState.IsValid)
            {
                // send email
            }
            else
            {
                // show error
            }
            return View();
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            //throw new InvalidOperationException("Bad thing just happened!");
            return View();
        }
    }
}
