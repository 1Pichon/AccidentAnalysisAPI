using AccidentAnalysisAPI.Modelo;
using ExcelDataReader;
using System.Data;

namespace AccidentAnalysisAPI.Repositorio
{
    public class AccidentDataService
    {
        public List<AccidentRecord> LoadAccidentsData(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var records = new List<AccidentRecord>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();

                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        records.Add(new AccidentRecord
                        {
                            Date = DateTime.Parse(row[0].ToString()),
                            AccidentType = row[1].ToString(),
                            ReportedCause = row[2].ToString(),
                            Department = row[3].ToString(),
                            TrainingReceived = row[4].ToString(),
                            Outcome = row[5].ToString(),
                        });
                    }
                }
            }

            return records;
        }
    }
}
