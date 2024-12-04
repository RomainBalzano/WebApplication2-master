using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ModelTrainer>();
builder.Services.AddSingleton<PredictionService>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var modelTrainer = scope.ServiceProvider.GetRequiredService<ModelTrainer>();
    var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Data", "Housing.csv");
    var modelPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Housing.zip");

    modelTrainer.TrainModel(dataPath, modelPath); 
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();