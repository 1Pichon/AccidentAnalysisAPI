namespace AccidentAnalysisAPI.Modelo
{
    public class AccidentRecord
    {
        public DateTime Date {  get; set; }
        public string AccidentType { get; set; }
        public string ReportedCause { get; set; }
        public string Department {  get; set; }
        public string TrainingReceived { get; set; }
        public string Outcome { get; set; }
    }
}
