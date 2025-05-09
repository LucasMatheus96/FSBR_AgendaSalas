using FSBR_AgendaSalas.Application.Configuration;
using FSBR_AgendaSalas.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace FSBR_AgendaSalas.MVC.Controllers
{
    public class SalaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;


        public SalaController(HttpClient httpClient, IOptions<ApiSettings> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.BaseUrl;
        }
        public async Task<IActionResult> Index()
        {
            
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/sala"); 

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var salas = JsonConvert.DeserializeObject<List<SalaViewModel>>(content);
                return View(salas);
            }

            // Caso a API não retorne sucesso
            return View(new List<SalaViewModel>());
        }

        // POST: /Sala/Create
        [HttpPost]
        public async Task<IActionResult> Create(SalaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/api/sala", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            // Você pode exibir erro retornado pela API se quiser
            ModelState.AddModelError(string.Empty, "Erro ao criar sala.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SalaViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/sala/{model.Id}", model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/sala/{id}");
            return RedirectToAction("Index");
        }
    }
}
