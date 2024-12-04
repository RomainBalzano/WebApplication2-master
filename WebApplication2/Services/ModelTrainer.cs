using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System;
using System.IO;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class ModelTrainer
    {
        private readonly MLContext _mlContext;

        public ModelTrainer()
        {
            _mlContext = new MLContext();
        }

        public void TrainModel(string dataPath, string modelPath)
        {
            Console.WriteLine($"Chargement des données depuis : {dataPath}");

           
            var data = _mlContext.Data.LoadFromTextFile<RealEstateData>(
                dataPath,
                hasHeader: true,
                separatorChar: ',');


            

            var pipeline = _mlContext.Transforms.CustomMapping<CustomMappingInput, CustomMappingOutput>(
                    MapData, contractName: "MapYesNoToFloat")
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding("Furnishingstatus"))
                .Append(_mlContext.Transforms.Concatenate("Features",
                    "Area", "Bedrooms", "Bathrooms", "Stories", "Parking", "Mainroad",
                    "Guestroom", "Basement", "Hotwaterheating", "Airconditioning", "Prefarea", "Furnishingstatus"))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(_mlContext.Regression.Trainers.FastTree(labelColumnName: "Price", featureColumnName: "Features"));



            Console.WriteLine("Entraînement du modèle...");

          
            var model = pipeline.Fit(data);

            
            var predictions = model.Transform(data);
            var metrics = _mlContext.Regression.Evaluate(predictions, labelColumnName: "Price");

            Console.WriteLine($"Métriques d'évaluation :");
            Console.WriteLine($"- RSquared : {metrics.RSquared}");
            Console.WriteLine($"- Mean Absolute Error : {metrics.MeanAbsoluteError}");
            Console.WriteLine($"- Mean Squared Error : {metrics.MeanSquaredError}");

            
            _mlContext.Model.Save(model, data.Schema, modelPath);

           
            

            
        }






        /// <summary>
        /// Mapping personnalisé pour convertir "yes"/"no" en float poour le Concatenate
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void MapData(CustomMappingInput input, CustomMappingOutput output)
        {
            output.Mainroad = input.Mainroad == "yes" ? 1f : 0f;
            output.Guestroom = input.Guestroom == "yes" ? 1f : 0f;
            output.Basement = input.Basement == "yes" ? 1f : 0f;
            output.Hotwaterheating = input.Hotwaterheating == "yes" ? 1f : 0f;
            output.Airconditioning = input.Airconditioning == "yes" ? 1f : 0f;
            output.Prefarea = input.Prefarea == "yes" ? 1f : 0f;
        }
    }






}
