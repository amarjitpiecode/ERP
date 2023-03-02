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
                ClassificationsName: $("#ClassificationsName").val().trim(),
                ClassificationsCode: $("#ClassificationsCode").val().trim(),
            }
            //StartProcess();
            $.ajax({
                type: "POST",
                url: "/Classifications/AddOrUpdateClassifications",
                data: { ClassificationsVM: data },
                success: function (data) {
                    console.log(data);
                    if (!data.isSuccess) {
                        //StopProcess();
                        $("#lblError").addClass("error").text(data.errors.map(c => c.errorDescription).toString()).show();
                    }
                    else {
                        window.location.href = '/Classifications/Index'
                    }
                }
            });
        }
    })
});
