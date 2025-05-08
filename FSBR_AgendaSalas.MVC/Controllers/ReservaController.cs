using FSBR_AgendaSalas.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FSBR_AgendaSalas.MVC.Controllers
{
    public class ReservaController : Controller
    {
        private readonly HttpClient _httpClient;


        public ReservaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            
            var response = await _httpClient.GetAsync("https://localhost:7081/api/Reserva"); 

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var reservas = JsonConvert.DeserializeObject<List<ReservaViewModel>>(content);
                return View("ReservaIndex", reservas);
            }

           
            return View(new List<UsuarioViewModel>());
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(ReservaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7081/api/Reserva", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            // Você pode exibir erro retornado pela API se quiser
            ModelState.AddModelError(string.Empty, "Erro ao criar sala.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReservaViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7081/api/Reserva/{model.Id}", model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7081/api/Reserva/{id}");
            return RedirectToAction("Index");
        }
    }
}
