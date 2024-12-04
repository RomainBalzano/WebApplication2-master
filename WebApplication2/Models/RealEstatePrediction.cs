using Microsoft.ML.Data;
namespace WebApplication2.Models
{
    public class RealEstatePrediction
    {
        [ColumnName("Score")]
        public float Price { get; set; }
    }
}
