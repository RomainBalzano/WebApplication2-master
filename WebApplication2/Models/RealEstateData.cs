using Microsoft.ML.Data;

namespace WebApplication2.Models
{
    public class RealEstateData
    {
        [LoadColumn(0)] public float Price { get; set; }
        [LoadColumn(1)] public float Area { get; set; }
        [LoadColumn(2)] public float Bedrooms { get; set; }
        [LoadColumn(3)] public float Bathrooms { get; set; }
        [LoadColumn(4)] public float Stories { get; set; }
        [LoadColumn(5)] public string Mainroad { get; set; }
        [LoadColumn(6)] public string Guestroom { get; set; }
        [LoadColumn(7)] public string Basement { get; set; }
        [LoadColumn(8)] public string Hotwaterheating { get; set; }
        [LoadColumn(9)] public string Airconditioning { get; set; }
        [LoadColumn(10)] public float Parking { get; set; }
        [LoadColumn(11)] public string Prefarea { get; set; }
        [LoadColumn(12)] public string Furnishingstatus { get; set; }
    }


}