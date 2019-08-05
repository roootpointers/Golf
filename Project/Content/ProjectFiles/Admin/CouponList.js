var Table;
$(document).ready(function () {
    $('html, body').animate({ scrollTop: 0 }, 'fast');
    $("#preloader").show(); 
    document.getElementById("CouponList").className = "active";
    //Initialize Select2 Elements
    $('.select2').select2();
    var start = moment();
    var end = moment(); 
    var dates = start.format('MM/DD/YYYY') + "$" + end.format('MM/DD/YYYY');
    Table = $('#example').DataTable({
        "serverSide": true,
        "processing": true,
        "ajax": "/api/WebApis/GetCouponTable?data=" + dates + "",
        "drawCallback": function (settings) { 
            $("#preloader").hide();
        },
        "lengthMenu": [[10, 25, 50, 100, 500, 1000, 5000], [10, 25, 50, 100, 500, 1000, 5000]]
    }); 
    $('#daterange-btn span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'))
});

$(function () {
    //Date range as a button 
    $('#daterange-btn').daterangepicker(
        {
            ranges: {
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
                'Uptill Now': [moment("1/1/2018".valueOf()), moment()]
            },
            startDate: moment(),
            endDate: moment()
        },
        function (start, end) {
            $("#preloader").show();
            var dates = start.format('MM/DD/YYYY') + "$" + end.format('MM/DD/YYYY');
            $("#example").dataTable().fnDestroy();
            Table = $('#example').DataTable({
                "serverSide": true,
                "processing": true,
                "ajax": "/api/WebApis/GetCouponTable?data=" + dates + "",
                "drawCallback": function (settings) {
                    LoopCheck = 0;
                    $("#preloader").hide();
                }
            });
            $('#daterange-btn span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'))
        }
    )
}) 

//Getting Record For Update
function Edit(id) {
    $("#UpdateModel").modal();
    $("#preloader").show();
    $.ajax({
        type: "GET",
        url: "/api/WebApis/GetSingleCouponById?Id=" + id + "", //URI
        dataType: "json",
        success: function (data) { 
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            document.getElementById("ID").value = data.ID; 
            document.getElementById("AddUnit").value = data.AdUnit; 
            document.getElementById("Coupon").value = data.Coupon;
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
                    url: "/api/WebApis/DeleteCoupon?Id=" + Id + "", //URI
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
 



