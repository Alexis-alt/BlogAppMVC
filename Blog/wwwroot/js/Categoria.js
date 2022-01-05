var dataTable;


$(document).ready(() => {


    cargarDataTable();


});


const cargarDataTable = () => {


    dataTable = $("#tableCategoria").dataTable({

        "ajax": {

            "url": "/Admin/Categorias/GetAll",
            "type": "GET",
            "datatype": "json"


        },

        "Columns": [

            { "data": "Id", "width": "5%" },
            { "data": "Nombre", "width": "50%" },
            { "data": "Orden", "width": "20%" },
            {

                "data": "Id",
                "render": (data) => {


                    return `<div class="text-center">
                            <a href='Admin/Categorias/Edit/${data}' class='btn btn-success text-white'  style='cursor:pointer;width:100px;'>
                            <i class='fa fa-edit'></i> Editar
                            </a>
                            &nbsp;
                            <a onclick=Delete("Admin/Categorias/Delete/${data}") class='btn btn-danger text-white'  style='cursor:pointer;width:100px;'>
                            <i class='fa fa-edit'></i> Eliminar
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







}