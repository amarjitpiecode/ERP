$(document).ready(function () {
    $("#lblError").removeClass("success").removeClass("error").text('');

    $("#btn-submit").on("click", function () {
        debugger
        $("#lblError").removeClass("success").removeClass("error").text('');
        var retval = true;
        $("#myForm .required").each(function () {
            if (!$(this).val()) {
                $(this).addClass("error");
                retval = false;
            }
            else {
                $(this).removeClass("error");
            }
        });

        if (retval) {
            var data = {
                id: $("#Id").val().trim(),
                EmployeeName: $("#EmployeeName").val().trim(),
                EmployeeAddress: $("#EmployeeAddress").val().trim(),
                EmployeePhone: $("#EmployeePhone").val().trim(),
            }
            //StartProcess();
            $.ajax({
                type: "POST",
                url: "/Employee/AddOrUpdateEmployee",
                data: { EmployeeVM: data },
                success: function (data) {
                    if (!data.isSuccess) {
                        //StopProcess();
                        $("#lblError").addClass("error").text(data.errors.map(c => c.errorDescription).toString()).show();
                    }
                    else {
                        window.location.href = '/Employee/Index'
                    }
                }
            });
        }
    })
});
