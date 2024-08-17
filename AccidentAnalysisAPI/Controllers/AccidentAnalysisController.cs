using AccidentAnalysisAPI.Modelo;
using AccidentAnalysisAPI.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace AccidentAnalysisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentAnalysisController : ControllerBase
    {
        private readonly AccidentDataService _dataService;
        private readonly ModelTrainer _modelTrainer;

        public AccidentAnalysisController(AccidentDataService dataService, ModelTrainer modelTrainer)
        {
            _dataService = dataService;
            _modelTrainer = modelTrainer;
        }

        [HttpPost("analyze")]
        public IActionResult AnalyzeAccidents([FromForm] IFormFile file)
        {
            // Guardar el archivo temporalmente
            var filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Cargar los datos desde el archivo Excel
            var data = _dataService.LoadAccidentsData(filePath);

            // Entrenar el modelo (esto es solo un ejemplo, podrías tener lógica más compleja aquí)
            var model = _modelTrainer.TrainModel(data);

            // Aquí deberías implementar la lógica para analizar los datos y generar los resultados
            // A continuación se muestra un ejemplo básico de cómo podrías devolver una lista de brechas críticas
            var breaches = IdentifyBreaches(data); // Implementa esta función según tu lógica

            // Retornar el resultado como un objeto JSON
            return Ok(new { Message = "Analysis complete", Breaches = breaches });
        }

        // Ejemplo de función que identificaría brechas críticas
        private List<string> IdentifyBreaches(IEnumerable<AccidentRecord> data)
        {
            // Aquí implementa la lógica para analizar los datos y determinar las brechas críticas
            var breaches = new List<string>();

            // Ejemplo: Añadir una brecha ficticia
            breaches.Add("Falta de entrenamiento en el uso de herramientas manuales");
            breaches.Add("Capacitación insuficiente en manejo de químicos peligrosos");

            return breaches;
        }
    }
}
