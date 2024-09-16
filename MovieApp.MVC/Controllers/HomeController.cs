using Microsoft.AspNetCore.Mvc;
using MovieApp.MVC.Models;
using RestSharp;
using System.Diagnostics;

namespace MovieApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        
       

        public IActionResult Index()
        {
           
            return View();
        }

    }
}
