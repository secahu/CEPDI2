
$(document).ready(function () {

    $("#btn_iniciar").show();
    $("#cargando").hide();

});

function login(e) {
    e.preventDefault();

    $("#btn_iniciar").hide();
    $("#cargando").show();


    var parametros = {
        "Usuario": form.usuario.value,
        "Password": form.password.value
    };

    $.ajax({
        url: ruta_api + api_usuarios + "Login",
        method: "POST",
        type: "POST",
        data: JSON.stringify(parametros),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            
            console.log(data);
            if (data.IdUsuario > 0) {

                window.location.assign("/Usuarios");

            } else {
                //alert(data.message);
                swal("¡Mal!", data.Mensaje, "warning");

            }
            $("#btn_iniciar").show();
            $("#cargando").hide();


        },
        error: function (error) {
            alert("Hubo un error!");
            console.log(error.responseText);
            $("#btn_iniciar").show();
            $("#cargando").hide();


        }
    });

}