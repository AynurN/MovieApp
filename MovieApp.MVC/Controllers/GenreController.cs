using Microsoft.AspNetCore.Mvc;
using MovieApp.MVC.ApiResponseMessages;
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
            var response = await restClient.ExecuteAsync<ApiResponseMessage<List<GenreGetVM>>>(request);
            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.ErrorMessage;
                return View();
            }
            return View(response.Data.Data);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var request = new RestRequest($"Genres/{id}", Method.Get);
            var response=await restClient.ExecuteAsync<ApiResponseMessage<GenreGetVM>>(request);
            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.ErrorMessage;
                return View();
            }
            return View(response.Data.Data);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(GenreCreateVM vm)
        {
            var request = new RestRequest("genres", Method.Post);
            foreach (var property in vm.GetType().GetProperties())
            {
                request.AddParameter(property.Name, property.GetValue(vm), ParameterType.RequestBody);
            }
            var response = await restClient.ExecuteAsync<ApiResponseMessage<GenreCreateVM>>(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("Name", response.Data.ErrorMessage);
                return View();
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
