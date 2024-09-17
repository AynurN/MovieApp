using Microsoft.AspNetCore.Mvc;
using MovieApp.MVC.ViewModels.Genre;
using RestSharp;

namespace MovieApp.MVC.Controllers
{
    public class GenreController : Controller
    {
        private readonly RestClient restClient;
        public GenreController()
        {
            restClient = new RestClient("https://localhost:7267/api/");
        }
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("Genres", Method.Get);
            var response = await restClient.ExecuteAsync<List<GenreGetVM>>(request);
            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.ErrorMessage;
                return View();
            }
            List<GenreGetVM> vm=response.Data;
            return View(vm);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var request = new RestRequest($"Genres/{id}", Method.Get);
            var response=await restClient.ExecuteAsync<GenreGetVM>(request);
            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.ErrorMessage;
                return View();
            }
            GenreGetVM vm=response.Data;
            return View(vm);
        }
    }
}
