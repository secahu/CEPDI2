var table = null;
var table_index = 0;
var columns = [
    {
        targets: table_index++,
        title: "Nombre",
        data: "Nombre"
    }, {
        targets: table_index++,
        title: "Concentración",
        data: "Concentracion"
    }, {
        targets: table_index++,
        title: "Forma farmacéutica",
        data: "FormaFarmaceutica"
    }, {
        targets: table_index++,
        title: "Precio",
        data: "Precio"
    }, {
        targets: table_index++,
        title: "Stock",
        data: "Stock"
    }, {
        targets: table_index++,
        title: "Presentación",
        data: "Presentacion"
    },
    {
        targets: table_index++,
        title: "Estatus",
        data: "bhabilitado",
        render: (data, type, r, m) => {
            return (type == "display") ? (data ? `<label class="text-success">Habilitado</label>` : `<label class="text-danger">Deshabilitado</label>`) : data
        }
    },
    {
        targets: table_index++,
        title: "Configuración",
        data: "IdMedicamento",
        render: (data, type, r, m) => {
            return (type == "display") ? `<button class="btn btn-primary" type="button" data-bs-toggle="modal" onclick="obtieneElemento(${data}); botones(2)" data-bs-target="#ModalAdd"><i class="mdi mdi-settings"></i>Editar</button>` +
                `<button type="button" class="btn btn-danger ml-4" onclick="borrar(${data});" class=""><i class="mdi mdi-delete-forever"></i>Eliminar</button>` : data
        }
    },
];


$(document).ready(function () {

    //$("#menu_medicamentos").addClass("active");
    recargaDataTable();
    carga_formas_farmaceuticas("forma", "");

});

function recargaDataTable() {
    if (table != null) {
        table.destroy();
    }
    table = $("#lista").DataTable({
        columnDefs: columns,
        aaSorting: [],
        pageLength: 10,
        ajax: {
            url: ruta_api + api_medicamentos + "/Get",
            dataSrc: ''
        },
        "language": lenguaje,
    });
}

function obtieneElemento(id) {


    console.log(id);
    $.ajax({
        url: ruta_api + api_medicamentos + "Get?id=" + id,
        type: "GET",
        //data:  JSON.stringify(parametros),
        //contentType: "json",
        dataType: 'json',
        success: function (data) {
            console.log(data);

            form.id.value = data.IdMedicamento;
            form.nombre.value = data.Nombre;
            form.concentracion.value = data.Concentracion;
            form.forma.value = data.IdFormaFarmaceutica;
            form.precio.value = data.Precio;
            form.stock.value = data.Stock;
            form.presentacion.value = data.Presentacion;
            form.estatus.checked = data.bhabilitado==1?true:false;

        },
        error: function (error) {
            alert("Hubo un error!");
            console.log(error.responseText);
        }
    });

}

function borrar(id) {

    var id = id;


    swal({
        title: "¿Estás seguro de eliminar el elemento?",
        text: "¡Una vez eliminando el elemento no se podrá recuperar!",
        type: "warning",
        showCancelButton: !0,
        confirmButtonColor: "#FF8800",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        //closeOnConfirm: !1
    })
        .then((willDelete) => {
            //console.log(willDelete);
            if (willDelete.dismiss !== 'cancel') {
                $.ajax({
                    url: ruta_api + api_medicamentos + "Delete?id=" + id,
                    type: "DELETE",
                    //data:  JSON.stringify(parametros),
                    //contentType: "json",
                    dataType: 'json',
                    success: function (data) {
                        //console.log(data);
                        if (data.Value) {
                            recargaDataTable();

                            swal("¡Elemento eliminado!", "", "success")
                        } else {
                            alert(data.message);
                        }
                    },
                    error: function (error) {
                        alert("Hubo un error!");
                        console.log(error.responseText);
                    }
                });
            } else {
                //swal("Your imaginary file is safe!");
            }
        });



}

function botones(opc) {
    if (opc == 1) {
        $("#nombre").val("");
        $("#concentracion").val("");
        $("#forma").val("");
        $("#precio").val("");
        $("#presentacion").val("");
        $("#estatus").prop("checked",false);
        $("#buttonAdd").show();
        $("#buttonSave").hide();
    } else if (opc == 2) {

        $("#buttonAdd").hide();
        $("#buttonSave").show();
    }
}

function btnAdd() {

    if (!ValidaCampos()) {
        return;
    }
    
    var parametros = {
        "Nombre": form.nombre.value,
        "Concentracion": form.concentracion.value,
        "IdFormaFarmaceutica": form.forma.value,
        "Precio": form.precio.value,
        "Stock": form.stock.value,
        "Presentacion": form.presentacion.value,
        "bhabilitado": form.estatus.checked?1:0,
    };

    $.ajax({
        url: ruta_api + api_medicamentos + "Post",
        method: "POST",
        type: "POST",
        data: JSON.stringify(parametros),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            if (data.Value) {

                recargaDataTable();
                $("#btncancelar").click();
                swal("¡Bien!", "Elemento agregado", "success")

            } else {
                alert(data.message);
            }
        },
        error: function (error) {
            alert("Hubo un error!");
            console.log(error.responseText);
        }
    });


}



function btnSave() {


    if (!ValidaCampos()) {
        return;
    }

    var parametros = {
        "IdMedicamento": form.id.value,
        "Nombre": form.nombre.value,
        "Concentracion": form.concentracion.value,
        "IdFormaFarmaceutica": form.forma.value,
        "Precio": form.precio.value,
        "Stock": form.stock.value,
        "Presentacion": form.presentacion.value,
        "bhabilitado": form.estatus.checked ? 1 : 0,
    };

    $.ajax({
        url: ruta_api + api_medicamentos + "Put?id=" + parametros.IdMedicamento,
        type: "Put",
        data: JSON.stringify(parametros),
        method: "Put",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            if (data.Value) {

                $("#btncancelar").click();
                recargaDataTable();
                swal("¡Bien!", "Elemento actualizado", "success")

            } else {
                alert(data.message);
            }
        },
        error: function (error) {
            alert("Hubo un error!");
            console.log(error.responseText);
        }
    });

}

function ValidaCampos() {

    if (!form.nombre.checkValidity()) {

        swal("¡Mal!", "El nombre no debe estar vacío");
        return false;

    } else if (!form.concentracion.checkValidity()) {

        swal("¡Mal!", "La concentraciòn no debe estar vacío");
        return false;

    } else if (!form.forma.checkValidity()) {

        swal("¡Mal!", "La forma no debe ir vacía");
        return false;

    } else if (!form.precio.checkValidity()) {

        swal("¡Mal!", "El precio no debe ir vacío");
        return false;

    } else if (!form.stock.checkValidity()) {

        swal("¡Mal!", "El stock no debe ir vacío");
        return false;

    } else if (!form.presentacion.checkValidity()) {

        swal("¡Mal!", "La presentación no debe ir vacía");
        return false;

    }
    return true;

}
