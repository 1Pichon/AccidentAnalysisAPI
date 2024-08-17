using AccidentAnalysisAPI.Modelo;
using Microsoft.ML;

namespace AccidentAnalysisAPI.Repositorio
{

    public class ModelTrainer
    {
        public ITransformer TrainModel(IEnumerable<AccidentRecord> data)
        {
            var mlContext = new MLContext();

            var dataView = mlContext.Data.LoadFromEnumerable(data);

            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding("AccidentTypeEncoded", "AccidentType")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("ReportedCauseEncoded", "ReportedCause"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("DepartmentEncoded", "Department"))
                .Append(mlContext.Transforms.Text.FeaturizeText("TrainingReceivedFeaturized", "TrainingReceived"))
                .Append(mlContext.Transforms.Concatenate("Features", "AccidentTypeEncoded", "ReportedCauseEncoded", "DepartmentEncoded", "TrainingReceivedFeaturized"))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(AccidentRecord.Outcome)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = pipeline.Fit(dataView);

            return model;
        }
    }
}
