namespace WebApplication2.Services
{
    using Microsoft.ML.Transforms;

    [CustomMappingFactoryAttribute("MapYesNoToFloat")]
    public class MapYesNoToFloatFactory : CustomMappingFactory<CustomMappingInput, CustomMappingOutput>
    {
        public override Action<CustomMappingInput, CustomMappingOutput> GetMapping()
        {
            return (input, output) =>
            {
                output.Mainroad = input.Mainroad == "yes" ? 1f : 0f;
                output.Guestroom = input.Guestroom == "yes" ? 1f : 0f;
                output.Basement = input.Basement == "yes" ? 1f : 0f;
                output.Hotwaterheating = input.Hotwaterheating == "yes" ? 1f : 0f;
                output.Airconditioning = input.Airconditioning == "yes" ? 1f : 0f;
                output.Prefarea = input.Prefarea == "yes" ? 1f : 0f;
            };
        }
    }

}
