using FSBR_AgendaSalas.Application.Configuration;
using FSBR_AgendaSalas.Domain.Shared;
using FSBR_AgendaSalas.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace FSBR_AgendaSalas.MVC.Controllers
{
    public class ReservaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ReservaController(HttpClient httpClient, IOptions<ApiSettings> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.BaseUrl;
        }
        public async Task<IActionResult> Index()
        {

            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Reserva");

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
            {
                TempData["Mensagem"] = "Dados inválidos. Tente novamente.";
                TempData["ModalAberto"] = "true";
                return RedirectToAction("Index");
            }


            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/api/Reserva", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var mensagemSucesso = JsonConvert.DeserializeObject<MensagemResponse>(content);
                TempData["Mensagem"] = mensagemSucesso?.Mensagem;

            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var mensagemErro = JsonConvert.DeserializeObject<MensagemResponse>(content);
                TempData["Mensagem"] = mensagemErro?.Erro;
            }

            // Você pode exibir erro retornado pela API se quiser

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReservaViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Reserva/{model.Id}", model);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var mensagemSucesso = JsonConvert.DeserializeObject<MensagemResponse>(content);
                TempData["Mensagem"] = mensagemSucesso?.Mensagem;

            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var mensagemErro = JsonConvert.DeserializeObject<MensagemResponse>(content);
                TempData["Mensagem"] = mensagemErro?.Erro;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Reserva/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var mensagemSucesso = JsonConvert.DeserializeObject<MensagemResponse>(content);
                TempData["Mensagem"] = mensagemSucesso?.Mensagem;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var mensagemErro = JsonConvert.DeserializeObject<MensagemResponse>(content);
                TempData["Mensagem"] = mensagemErro?.Erro;
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Cancelar(int id)
        {
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/Reserva/Cancelar/{id}", new StringContent(""));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var mensagemSucesso = JsonConvert.DeserializeObject<MensagemResponse>(content);
                TempData["Mensagem"] = mensagemSucesso?.Mensagem;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var mensagemErro = JsonConvert.DeserializeObject<MensagemResponse>(content);
                TempData["Mensagem"] = mensagemErro?.Erro;
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> GetSalas()
        {
            var response = await _httpClient.GetAsync("https://localhost:7081/api/Sala");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var salas = JsonConvert.DeserializeObject<List<SalaViewModel>>(content);
                return Json(salas);
            }
            return Json(new List<SalaViewModel>());
        }

        // Método para buscar usuários
        public async Task<IActionResult> GetUsuarios()
        {
            var response = await _httpClient.GetAsync("https://localhost:7081/api/Usuario");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(content);
                return Json(usuarios);
            }
            return Json(new List<UsuarioViewModel>());
        }
    }
}
