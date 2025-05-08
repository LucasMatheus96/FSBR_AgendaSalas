using FSBR_AgendaSalas.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FSBR_AgendaSalas.MVC.Controllers
{
    public class SalaController : Controller
    {
        private readonly HttpClient _httpClient;

        public SalaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            
            var response = await _httpClient.GetAsync("https://localhost:7081/api/sala"); 

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var salas = JsonConvert.DeserializeObject<List<SalaViewModel>>(content);
                return View(salas);
            }

            // Caso a API não retorne sucesso
            return View(new List<SalaViewModel>());
        }
    }
}
