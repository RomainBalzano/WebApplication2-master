using Microsoft.AspNetCore.Mvc;

using System.IO;
using Microsoft.ML;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelTrainer _modelTrainer;
        private readonly PredictionService _predictionService;

        public HomeController(ModelTrainer modelTrainer, PredictionService predictionService)
        {
            _modelTrainer = modelTrainer;
            _predictionService = predictionService;
        }

        public IActionResult Index()
        {
            return View();
        }

       

        [HttpPost]
        [HttpPost]
        public IActionResult PredictPrice(RealEstateData input)
        {
           
            if (ModelState.IsValid)
            {
              
                var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Housing.csv");
                var data = _predictionService.MLContext.Data.LoadFromTextFile<RealEstateData>(dataPath, hasHeader: true, separatorChar: ',');
                var closestMatch = _predictionService.MLContext.Data.CreateEnumerable<RealEstateData>(data, reuseRowObject: false)
                .OrderBy(d => Math.Abs(d.Bedrooms - input.Bedrooms) +
                              Math.Abs(d.Bathrooms - input.Bathrooms) +
                              Math.Abs(d.Stories - input.Stories) +
                              Math.Abs(d.Area - input.Area))
                .FirstOrDefault();


                var predictedPrice = _predictionService.Predict(input);

              
    

                var closestMatchPrice = closestMatch != null ? closestMatch.Price : (float?)null;

                return Json(new
                {
                    predictedPrice,
                    closestMatchPrice
                });
            }

   
            return BadRequest(new { error = "Invalid input data. Please check your entries." });
        }
        [HttpGet]
       
        public IActionResult Error()
        {
            return View();
        }
       
      
       


    }
}