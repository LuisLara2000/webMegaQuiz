using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using webMegaQuiz.Models;
using System.Security.Policy;

namespace webMegaQuiz.Controllers
{
    public class PreguntasController : Controller
    {
        public string respuestaActualCorrecta;
        public int idPreguntaActual;
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

        public async Task<IActionResult> ListarAleatorio()
        {
            // Llamamos a la API //
            var respuesta = await _httpClient.GetAsync("api/preguntas/ObtenerListaPreguntasAleatoreas");
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
        public async Task<IActionResult> ObtenerUnaPregunta(int sig)
        {
            /*
            TempData["pA"] = 1;
            TempData["pT"] = 3;
            
            await ObtenerUnaPreguntaAleatorea();
            return View("ObtenerUnaPregunta");*/

            // obtengo la pregunta original
            if (sig != -1)
            {
                // Llamamos a la API //
                var respuesta = await _httpClient.GetAsync("api/preguntas/ObtenerPregunta/" + sig.ToString());
                if (respuesta.IsSuccessStatusCode)
                {
                    // obtengo y decodifico la respuesta
                    var respuestaContenido = await respuesta.Content.ReadAsStringAsync();
                    var pregunta = JsonConvert.DeserializeObject<PreguntaModels>(respuestaContenido);
                    // Ordeno de manera aleatoria las respuestas de la pregunta
                    List<string> rOriginal = new List<string>();
                    PreguntaModels rDesordenadas = new PreguntaModels();
                    Random random = new Random();
                    // obtengo las respuestas ordenadas
                    rOriginal.Add(pregunta.rCorrecta.ToUpper());
                    rOriginal.Add(pregunta.rIncorrecta1.ToUpper());
                    rOriginal.Add(pregunta.rIncorrecta2.ToUpper());
                    rOriginal.Add(pregunta.rIncorrecta3.ToUpper());
                    rOriginal.Add(pregunta.rIncorrecta4.ToUpper());
                    respuestaActualCorrecta = pregunta.rCorrecta.ToUpper();
                    idPreguntaActual = pregunta.idPregunta;
                    for (int i = 1; i <= 5; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                rDesordenadas.rIncorrecta1 = rOriginal[random.Next(rOriginal.Count)].ToUpper();
                                rOriginal.Remove(rDesordenadas.rIncorrecta1);
                                break;
                            case 2:
                                rDesordenadas.rIncorrecta2 = rOriginal[random.Next(rOriginal.Count)].ToUpper();
                                rOriginal.Remove(rDesordenadas.rIncorrecta2);
                                break;
                            case 3:
                                rDesordenadas.rIncorrecta3 = rOriginal[random.Next(rOriginal.Count)].ToUpper();
                                rOriginal.Remove(rDesordenadas.rIncorrecta3);
                                break;
                            case 4:
                                rDesordenadas.rIncorrecta4 = rOriginal[random.Next(rOriginal.Count)].ToUpper();
                                rOriginal.Remove(rDesordenadas.rIncorrecta4);
                                break;
                            case 5:
                                rDesordenadas.rIncorrecta5 = rOriginal[random.Next(rOriginal.Count)].ToUpper();
                                rOriginal.Remove(rDesordenadas.rIncorrecta5);
                                break;
                        }
                    }
                    // agrego el valor de la pregunta
                    rDesordenadas.pregunta = pregunta.pregunta.ToUpper();
                    rDesordenadas.rCorrecta = pregunta.rCorrecta.ToUpper();
                    rDesordenadas.idPregunta = pregunta.idPregunta;
                    return View(rDesordenadas);
                }
                else
                {
                    await Listar();
                    return View("titulo");
                    
                }
            }
            else
            {
                await Listar();
                return View("Resultados");
            }

            

        }

        public async Task<IActionResult> ObtenerUnaPreguntaAleatorea()
        {
            // obtengo la pregunta original
            // Llamamos a la API //
            var respuesta = await _httpClient.GetAsync("api/preguntas/ObtenerPreguntaAleatorea");
            if (respuesta.IsSuccessStatusCode)
            {
                // obtengo y decodifico la respuesta
                var respuestaContenido = await respuesta.Content.ReadAsStringAsync();
                var pregunta = JsonConvert.DeserializeObject<PreguntaModels>(respuestaContenido);
                // Ordeno de manera aleatoria las respuestas de la pregunta
                List<string> rOriginal = new List<string>();
                PreguntaModels rDesordenadas = new PreguntaModels();
                Random random = new Random();
                // obtengo las respuestas ordenadas
                rOriginal.Add(pregunta.rCorrecta);
                rOriginal.Add(pregunta.rIncorrecta1);
                rOriginal.Add(pregunta.rIncorrecta2);
                rOriginal.Add(pregunta.rIncorrecta3);
                rOriginal.Add(pregunta.rIncorrecta4);
                respuestaActualCorrecta = pregunta.rCorrecta;
                idPreguntaActual = pregunta.idPregunta;
                for (int i = 1; i <= 5; i++)
                {
                    switch (i)
                    {
                        case 1:
                            rDesordenadas.rIncorrecta1 = rOriginal[random.Next(rOriginal.Count)];
                            rOriginal.Remove(rDesordenadas.rIncorrecta1);
                            break;
                        case 2:
                            rDesordenadas.rIncorrecta2 = rOriginal[random.Next(rOriginal.Count)];
                            rOriginal.Remove(rDesordenadas.rIncorrecta2);
                            break;
                        case 3:
                            rDesordenadas.rIncorrecta3 = rOriginal[random.Next(rOriginal.Count)];
                            rOriginal.Remove(rDesordenadas.rIncorrecta3);
                            break;
                        case 4:
                            rDesordenadas.rIncorrecta4 = rOriginal[random.Next(rOriginal.Count)];
                            rOriginal.Remove(rDesordenadas.rIncorrecta4);
                            break;
                        case 5:
                            rDesordenadas.rIncorrecta5 = rOriginal[random.Next(rOriginal.Count)];
                            rOriginal.Remove(rDesordenadas.rIncorrecta5);
                            break;
                    }
                }
                // agrego el valor de la pregunta
                rDesordenadas.pregunta = pregunta.pregunta;
                rDesordenadas.rCorrecta = pregunta.rCorrecta;
                rDesordenadas.idPregunta = pregunta.idPregunta;
                // cargo la info en menu
                ViewBag.cantidadPreguntasActual = TempData.Peek("pA");
                ViewBag.cantidadPreguntasTotal = TempData.Peek("pT");
                return View("ObtenerUnaPregunta",rDesordenadas);
            }
            else
            {
                return View("Inicio");
            }
        }
        public IActionResult validarRespuesta(string respuesta, string c, int cantidadPreguntas, int preguntaActual)
        {
            preguntaActual++;
            TempData["pA"] = preguntaActual;
            // si es menos de la cantidad maxima de preguntas
            // si esta correcto
            if (respuesta == c)
            {
                return View("Correcto");
            }
            else
            {
                return View("Incorrecto");
            }

        }

        public async Task<IActionResult> continuar()
        {
             
            int pa = Convert.ToInt32(TempData.Peek("pA"));
            int pt = Convert.ToInt32(TempData.Peek("pT"));
            if(pa<pt+1)
            {
                await ObtenerUnaPreguntaAleatorea();
                return View("ObtenerUnaPregunta");
            }
            else
            {
                return View("Resultados");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Titulo()
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
                return View(preguntas);
            }
            else
            {
                return View(new List<PreguntaModels>());
            }
            
        }
        
        public IActionResult Resultados()
        {
            return View();
        }
    }
}
