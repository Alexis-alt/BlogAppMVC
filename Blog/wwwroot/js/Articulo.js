var dataTable;

$(document).ready(function () {


    //Al cargar la pagina el método cargarDataTable se ejecuta automaticamente 
    //Dicho método hace una llamada AJAX al método GetAll del Controller, obteniendo como resultado un JSON con el cual se completa el DataTable 

    cargarDatatable();

});


function cargarDatatable() {
    dataTable = $("#tableArticulo").DataTable({

        //Se configura llamada AJAX 
        "ajax": {
            "url": "/Admin/Articulos/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        //Configuración de columnas

        "columns": [
            { "data": "IdArticulo", "width": "5%" },
            { "data": "nombre", "width": "25%" },
            { "data": "categoria.nombre", "width": "15%" },
            { "data": "fechaCreacion", "width": "15%" },
            {
                "data": "IdArticulo",
                "render": function (data) {

                    return `<div class="text-center">
                            <a href='/Admin/Articulos/Edit/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-edit'></i> Editar
                            </a>
                            &nbsp;
                            <a onclick=Delete("/Admin/Articulos/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-trash-alt'></i> Borrar
                            </a>
                            `;
                }, "width": "30%"
            }
        ],

        "language": {
            "emptyTable": "No hay registros"
        },
        "width": "100%"
    });
}


function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}
