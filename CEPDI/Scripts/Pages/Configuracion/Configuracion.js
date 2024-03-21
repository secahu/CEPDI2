var api_formas_farmaceuticas = "FormasFarmaceuticasApi/";
var api_medicamentos = "MedicamentosApi/";
var api_usuarios = "UsuariosApi/";

var lenguaje = {
    "decimal": "",
    "emptyTable": "No hay información",
    "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
    "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
    "infoFiltered": "(Filtrado de _MAX_ total entradas)",
    "infoPostFix": "",
    "thousands": ",",
    "lengthMenu": "Mostrar _MENU_ Entradas",
    "loadingRecords": "Cargando...",
    "processing": "Procesando...",
    "search": "Buscar:",
    "zeroRecords": "Sin resultados encontrados",
    "paginate": {
        "first": "Primero",
        "last": "Ultimo",
        "next": "Siguiente",
        "previous": "Anterior"
    }
};


function carga_formas_farmaceuticas(id_select, selected) {


    var sel = document.getElementById(id_select);
    sel.length = 1;

    $.ajax({
        url: ruta_api + api_formas_farmaceuticas+ "Get",
        type: "Get",
        //data: JSON.stringify(parametros),
        contentType: "json",
        dataType: 'json',
        success: function (data) {
            console.log(data);

            if (data.length > 0) {

                data.forEach(function (valor, indice, array) {
                    
                    if (selected != null && selected != "" && selected == valor.IdFormaFarmaceutica) {

                        var sel = document.getElementById(id_select);
                        var opt = document.createElement('option');

                        opt.appendChild(document.createTextNode(valor.Nombre));
                        opt.value = valor.IdFormaFarmaceutica;
                        opt.selected = true;
                        sel.appendChild(opt);

                    } else {

                        var sel = document.getElementById(id_select);
                        var opt = document.createElement('option');

                        opt.appendChild(document.createTextNode(valor.Nombre));
                        opt.value = valor.IdFormaFarmaceutica;
                        sel.appendChild(opt);

                    }
                });

            }

        },
        error: function (error) {
            alert("Hubo un error!");
            console.log(error);
            console.log(error.responseText);
        }
    });

}