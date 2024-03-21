var table = null;
var table_index = 0;
var columns = [
    {
        targets: table_index++,
        title: "Nombre",
        data: "Nombre"
    }, {
        targets: table_index++,
        title: "Usuario",
        data: "Usuario"
    },
    {
        targets: table_index++,
        title: "Estatus",
        data: "Estatus",
        render: (data, type, r, m) => {
            return (type == "display") ? (data ? `<label class="text-success">Habilitado</label>` : `<label class="text-danger">Deshabilitado</label>`) : data
        }
    },
    {
        targets: table_index++,
        title: "Configuración",
        data: "IdUsuario",
        render: (data, type, r, m) => {
            return (type == "display") ? `<button class="btn btn-primary" type="button" data-bs-toggle="modal" onclick="obtieneElemento(${data}); botones(2)" data-bs-target="#ModalAdd"><i class="mdi mdi-settings"></i>Editar</button>` +
                `<button type="button" class="btn btn-danger ml-4" onclick="borrar(${data});" class=""><i class="mdi mdi-delete-forever"></i>Eliminar</button>` : data
        }
    },
];


$(document).ready(function () {

    //$("#menu_usuarios").addClass("active");
    recargaDataTable();

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
            url: ruta_api + api_usuarios + "/Get",
            dataSrc: ''
        },
        "language": lenguaje,
    });
}

function obtieneElemento(id) {


    console.log(id);
    $.ajax({
        url: ruta_api + api_usuarios + "Get?id=" + id,
        type: "GET",
        //data:  JSON.stringify(parametros),
        //contentType: "json",
        dataType: 'json',
        success: function (data) {
            console.log(data);

            form.id.value = data.IdUsuario;
            form.nombre.value = data.Nombre;
            form.usuario.value = data.Usuario;
            form.password.value = data.Password;
            form.idperfil.value = data.IdPerfil;
            form.estatus.checked = data.Estatus==1?true:false;

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
    }).then((willDelete) => {
            //console.log(willDelete);
            if (willDelete.dismiss !== 'cancel') {
                $.ajax({
                    url: ruta_api + api_usuarios + "Delete?id=" + id,
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
        $("#usuario").val("");
        $("#password").val("");
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
        "Usuario": form.usuario.value,
        "Password": form.password.value,
        "Estatus": form.estatus.checked ? 1 : 0,
        "IdPerfil": form.idperfil.value
    };
    console.log(parametros);
    $.ajax({
        url: ruta_api + api_usuarios + "Post",
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
        "IdUsuario": form.id.value,
        "Nombre": form.nombre.value,
        "Usuario": form.usuario.value,
        "Password": form.password.value,
        "Estatus": form.estatus.checked ? 1 : 0,
        "IdPerfil": form.idperfil.value,

    };
    console.log(parametros);

    $.ajax({
        url: ruta_api + api_usuarios + "Put?id=" + parametros.IdUsuario,
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

function ValidaCampos(){

    if (!form.nombre.checkValidity()) {

        swal("¡Mal!", "El nombre no debe estar vacío");
        return false;

    } else if (!form.usuario.checkValidity()) {

        swal("¡Mal!","El usuario no debe estar vacío");
        return false;

    }else if (!form.password.checkValidity()) {

        swal("¡Mal!","El password debe contener almenos 1 Caracter especial, 1 Número, 1 Letra mayuscula, 1 Letra minuscula y ser de una longitud mínima de 8");
        return false;

    } else if (!form.idperfil.checkValidity()) {

        swal("¡Mal!", "El perfil no debe estar vacío");
        return false;

    }
    return true;

}