using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using webMegaQuiz.Models;

namespace webMegaQuiz.Controllers
{
    public class PreguntasController : Controller
    {
        /////////////////////////////////////
        // Declaro un cliente  //
        /////////////////////////////////////
        private readonly HttpClient _httpClient;

        /////////////////////////////////////
        // Creo el contructor  //
        /////////////////////////////////////
        public PreguntasController(IHttpClientFactory httpClientFactory)
        {
            // Inicializo el cliente y le paso la url basica
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5212/");
        }


        public async Task<IActionResult> Listar()
        {
            // Llamamos a la API //
            var respuesta = await _httpClient.GetAsync("api/preguntas/ListaPreguntas");
            // Validamos si la respuesta fue exitosa 
            if (respuesta.IsSuccessStatusCode)
            {
                // obtengo y decodifico la respuesta
                var respuestaContenido = await respuesta.Content.ReadAsStringAsync();
                //var preguntas = JsonConvert.DeserializeObject<List<PreguntaModels>>(respuestaContenido);
                //var preguntas = JsonConvert.DeserializeObject<PreguntaModels>(respuestaContenido);
                var preguntas = JsonConvert.DeserializeObject<IEnumerable<PreguntaModels>>(respuestaContenido);
                // devuelvo una vista
                return View("Listar", preguntas);
            }
            return View(new List<PreguntaModels>());
        }

        public async Task<IActionResult> ObtenerUnaPregunta(int id)
        {
            // Llamamos a la API //
            var respuesta = await _httpClient.GetAsync("api/preguntas/ObtenerPregunta/"+id.ToString());
            if (respuesta.IsSuccessStatusCode)
            {
                // obtengo y decodifico la respuesta
                var respuestaContenido = await respuesta.Content.ReadAsStringAsync();
                var pregunta = JsonConvert.DeserializeObject<PreguntaModels>(respuestaContenido);
                // devuelvo una vista
                return View(pregunta);
            }
            else
            {
                return View("Inicio");
            }
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
