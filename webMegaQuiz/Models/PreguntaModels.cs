
using System.Diagnostics.Metrics;
using System.Net.Sockets;

namespace webMegaQuiz.Models
{
    public class PreguntaModels
    {
        // Es la tabla que esta en la base de datos
        public int idPregunta {  get; set; }
        public string pregunta {  get; set; } = null!;
        public string rCorrecta { get; set; } = null!;
        public string rIncorrecta1 { get; set; } = null!;
        public string rIncorrecta2 { get; set; } = null!;
        public string rIncorrecta3 { get; set; } = null!;
        public string rIncorrecta4 { get; set; } = null!;
        public string rIncorrecta5 { get; set; } = null!;
        public int estado { get; set; }
        public int dificultad { get; set; }

    }
}
