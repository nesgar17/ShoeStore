$(document).ready(function () {
    $("#IdState").change(function () {
        $("#IdMunicipality").empty();
        $("#IdMunicipality").append('<option value="0">[Selecciona un Municipio...]</option>');
        $.ajax({
            type: 'POST',
            url: Url,
            dataType: 'json',
            data: { IdState: $("#IdState").val() },
            success: function (data) {
                $.each(data, function (i, data) {
                    $("#IdMunicipality").append('<option value="'
                        + data.IdMunicipality + '">'
                        + data.Description + '</option>');
                });
            },
            error: function (ex) {
                alert('Fallo la consulta de municipios.' + ex);
            }
        });
        return false;
    })
});

$(document).ready(function () {
    $("#IdMunicipality").change(function () {
        $("#IdColony").empty();
        $("#IdColony").append('<option value="0">[Selecciona una Colonia...]</option>');
        $.ajax({
            type: 'POST',
            url: Urlc,
            dataType: 'json',
            data: { IdMunicipality: $("#IdMunicipality").val() },
            success: function (data) {
                $.each(data, function (i, data) {
                    $("#IdColony").append('<option value="'
                        + data.IdColony + '">'
                        + data.Description + '</option>');
                });
            },
            error: function (ex) {
                alert('Fallo la consulta de municipios.' + ex);
            }
        });
        return false;
    })
});