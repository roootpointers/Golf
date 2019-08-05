var Table;
$(document).ready(function () {
    $('html, body').animate({ scrollTop: 0 }, 'fast');
    document.getElementById("Admins").className = "active";
    $("#preloader").show();
    Table = $('#example').DataTable({
        "serverSide": true,
        "processing": true,
        "ajax": "/api/WebApis/GetAdminTable",
        "drawCallback": function (settings) {
            $("#preloader").hide();
        },
        "lengthMenu": [[10, 25, 50, 100, 500, 1000, 5000], [10, 25, 50, 100, 500, 1000, 5000]]
    }); 
});

//Form For New Record
function Add() {
    $("#preloader").show();
    document.getElementById("BtnAdmin").disabled = true;
    if ($("#Name").val() != null) { 
        var Admin = {
            Occupation: "Admin", FullName: $("#Name").val(),
            UserName: $("#UserName1").val(), Password: $("#Password1").val()
        };
        var AdminData = JSON.stringify(Admin);
        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "/api/WebApis/PostAdmin",
            contentType: 'application/json; charset=utf-8',
            processData: false,
            dataType: "json",
            data: AdminData,
            success: function (data, textStatus, xhr) {
                if (data == 0) {
                    $("#preloader").hide();
                    document.getElementById("BtnAdmin").disabled = false;
                    bootbox.alert('UserName Already exists');
                    $('#Adminform')[0].reset();
                }
                else {
                    var Imageform = new FormData();
                    Imageform.append("ImageId", data);
                    var files = $("#AdminImage").get(0).files;
                    Imageform.append("Picture", files[0]);
                    // Add the uploaded image content to the form data collection
                    if (files[0] != null) {
                        var ajaxRequest = $.ajax({
                            type: "POST",
                            url: "/api/WebApis/PostImagesAdmin",
                            contentType: false,
                            processData: false,
                            data: Imageform,
                            success: function (data, textStatus, xhr) {
                                Table.ajax.reload(null, false);
                            }
                        });
                    }
                    else {
                        Table.ajax.reload(null, false);
                    }
                    document.getElementById("BtnAdmin").disabled = false;
                    $('#Adminform')[0].reset();
                    $("#AddModel").modal('hide');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $("#preloader").hide();
                document.getElementById("BtnAdmin").disabled = false;
                bootbox.alert('Server is not responding! Try Again Later');
                $('#Adminform')[0].reset();
            }
        });
        return false;
    }
    else {
        bootbox.alert("Make Sure All Fields with * are filled!");
        document.getElementById("BtnAdmin").disabled = false;
        return false;
    }
}

//Getting Record For Update
function Edit(id) {
    $("#UpdateModel").modal();
    $("#preloader").show();
    $.ajax({
        type: "GET",
        url: "/api/WebApis/GetSingleAdminById?Id=" + id + "", //URI
        dataType: "json",
        success: function (data) {
            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            document.getElementById("AdminId").value = data.ID;
            document.getElementById("NameUpdate").value = data.FullName;
            document.getElementById("UserNameUpdate").value = data.UserName;
            document.getElementById("PasswordUpdate").value = data.Password;  
            $("#preloader").hide();
        },
        error: function (xhr) {
            $("#preloader").hide();
            bootbox.alert('Internel Error...');
        }
    });
}

//Delete Record
function Delete(Id) {
    bootbox.confirm({
        title: "Remove entry?",
        message: "There might be some related record to this entry, If you delete you will lose some data. Are you sure you want to delete? This cannot be undone.",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Cancel'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Confirm'
            }
        },
        callback: function (result) {
            if (result) {
                $("#preloader").show(); 
                $.ajax({
                    type: 'DELETE',
                    url: "/api/WebApis/DeleteAdmin?Id=" + Id + "", //URI
                    dataType: "json",
                    contentType: "application/json",
                    success: function (data, textStatus, xhr) {
                        if (data == 0) {
                            $("#preloader").hide();
                            bootbox.alert('Try Again! Later...');
                        }
                        else {
                            Table.ajax.reload(null, false);
                            $("#preloader").hide();
                        }
                    },
                    error: function (xhr) {
                        $("#preloader").hide();
                        bootbox.alert('Error in Operation Try Again Later');
                    }
                });
            }
        }
    });   
}

//Form For Update Record
function Update() {
    debugger;
    $("#preloader").show();
    document.getElementById("BtnAdminUpdate").disabled = true;
    if ($("#NameUpdate").val() != null) { 
        var Admin;
        try {
            Admin = {
                ID: $("#AdminId").val(),
                Occupation: "Admin", FullName: $("#NameUpdate").val(),
                UserName: $("#UserNameUpdate").val(), Password: $("#PasswordUpdate").val()
            };
        } catch (error) {
            Admin = {
                ID: $("#AdminId").val(),
                Occupation: "Admin", FullName: $("#NameUpdate").val(),
                UserName: $("#UserNameUpdate").val(), Password: $("#PasswordUpdate").val(), 
            };
        }
        var CarreerData = JSON.stringify(Admin);
        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "/api/WebApis/UpdateAdmin",
            contentType: 'application/json; charset=utf-8',
            processData: false,
            dataType: "json",
            data: CarreerData,
            success: function (data, textStatus, xhr) {
                if (data == 0) {
                    $("#preloader").hide();
                    document.getElementById("BtnAdminUpdate").disabled = false;
                    bootbox.alert('UserName Already exists');
                }
                else {
                    var Imageform = new FormData();
                    Imageform.append("ImageId", data);
                    var files = $("#ImageUpdate").get(0).files;
                    Imageform.append("Picture", files[0]);
                    // Add the uploaded image content to the form data collection
                    if (files[0] != null) {
                        var ajaxRequest = $.ajax({
                            type: "POST",
                            url: "/api/WebApis/PostImagesAdmin",
                            contentType: false,
                            processData: false,
                            data: Imageform,
                            success: function (data, textStatus, xhr) {
                                Table.ajax.reload(null, false);
                            }
                        });
                    }
                    else {
                        Table.ajax.reload(null, false);
                    }
                    document.getElementById("BtnAdminUpdate").disabled = false;
                    $('#UpdateForm')[0].reset();
                    $("#UpdateModel").modal('hide');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                document.getElementById("BtnAdminUpdate").disabled = false;
                $("#preloader").hide();
                bootbox.alert('Server is not responding! Try Again Later');
            }
        });
        return false;
    }
    else {
        $("#preloader").hide();
        bootbox.alert("Make Sure All Fields with * are filled!");
        document.getElementById("BtnAdminUpdate").disabled = false;
        return false;
    }
}