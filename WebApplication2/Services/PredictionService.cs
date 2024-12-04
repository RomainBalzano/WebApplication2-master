using Microsoft.ML;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class PredictionService
    {
        public MLContext MLContext { get; }
        private readonly ITransformer _model;

        public PredictionService()
        {
            MLContext = new MLContext();
            var modelPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Housing.zip");
            _model = MLContext.Model.Load(modelPath, out _);
        }

        public float Predict(RealEstateData inputData)
        {
           
           
            var inputDataView = MLContext.Data.LoadFromEnumerable(new[] { inputData });

        
            var transformedData = _model.Transform(inputDataView);

         
            var predictions = MLContext.Data.CreateEnumerable<RealEstatePrediction>(transformedData, reuseRowObject: false).ToList();

            var prediction = predictions.FirstOrDefault();

            if (prediction != null)
            {
             
                return prediction.Price;
            }
            else
            {
                Console.WriteLine("La prédiction a échoué.");
                return 0;
            }
        }
    }
}
