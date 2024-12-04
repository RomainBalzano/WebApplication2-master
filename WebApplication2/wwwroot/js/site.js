$(document).ready(function () {
    $('#predictionForm').submit(function (event) {
        event.preventDefault();

        const formData = $(this).serializeArray();
        console.log(formData);

        const submitButton = $(this).find("button[type='submit']");
        submitButton.prop("disabled", true).text("Chargement...");

        $.ajax({
            url: '/Home/PredictPrice',
            type: 'POST',
            data: $.param(formData),
            success: function (response) {
                $('#predictionResult').text(response.predictedPrice + ' $');
                $('#resultModal').modal('show');
            },
            error: function () {
                $('#predictionResult').text('Une erreur est survenue lors de la prédiction.');
                $('#resultModal').modal('show');
            },
            complete: function () {
                submitButton.prop("disabled", false).text("Prédire le prix");
            }
        });
    });
});
