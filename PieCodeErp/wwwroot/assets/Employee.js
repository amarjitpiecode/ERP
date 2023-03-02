var table;
$("document").ready(function () {
    loadAllEmployees();
 
    // Display a success toast, with a title
//    Toast.fire({
//        icon: 'success',
//        title: 'Your Level is 3.'
//    })
//})
$("#datatable-basic").on("click", "a#btn-delete", function () {
    var id = $(this).data('id');
    $('#deleteModal').data('id', id).modal('show');
    $('#deleteModal').modal('show');
});
$('#delete-btn').click(function () {
    var id = $('#deleteModal').data('id');
    $.ajax({
        type: "GET",
        url: "/Employee/DeleteEmployee",
        data: { id: id },
        success: function (response) {
            if (!response.isSuccess) {
                $('#deleteModal').modal('hide');
            }
            else {
                $('#deleteModal').modal('hide');
                table.ajax.reload()
                funToastr(true, response.message);
            }
        },
        error: function (error) {
            //toastr.error(error)
        }
    });
});


function loadAllEmployees() {
    var url = "/Employee/GetAllEmployee"
    table = $("#datatable-basic").DataTable({

        "searching": true,
        "serverSide": true,
        "bFilter": true,
        "orderMulti": false,
        "ajax": {
            url: url,
            type: "POST",
            datatype: "json"
        },
        
        "columns": [
            {
                "data": "employeeName"
            },
            {
                "data": "employeeAddress"
            },
            {
                "data": "employeePhone"
            },
            {
                "render": function (data, type, full, meta) {
                    return ` <a href="/Employee/Edit/` + full.employeeId + `" data-id="` + full.employeeId + `" class="btn btn-success btn-sm" title="Edit">
                                    <i class="fa fa-edit"></i>
                             </a>
                             <a href="javascript:void(0)" id="btn-delete" data-id="`+ full.employeeId + `" class="btn btn-danger btn-sm" title="Delete">
                                    <i class="fa fa-trash"></i>
                             </a>`;
                }
            }

        ],
        //"order": [[3, 'desc']],
        //"dom": ' <"search"f><"top"l>rt<"bottom"ip><"clear">'
    });
    //$("#myTable_filter").css("float", "right");
}

