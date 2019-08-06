$(document).ready(function () {
    $('html, body').animate({ scrollTop: 0 }, 'fast'); 
    $.ajax({
        type: "GET",
        url: "/api/WebApis/GetCurrentUserInfo", //URI
        dataType: "json",
        success: function (data) {
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            document.getElementById("LayoutPageFullName1").innerHTML = data.FullName;
            var level = "";
            try {
                level = data.SecurityLevel.levelName;

            } catch (error) {
                if (data.Occupation == "MasterAdmin" && data.SecurityLevelID == null)
                    level = "Master";
                else if (data.Occupation != "MasterAdmin" && data.SecurityLevelID == null)
                    level = "Others";
            } 
            document.getElementById("LayoutPageSecurityLevel1").innerHTML = "Security Level: " + level;
            if (data.Image != null)
                document.getElementById("LayoutPageProfilePic1").src = data.Image;

            document.getElementById("LayoutPageFullName2").innerHTML = data.FullName;
            document.getElementById("LayoutPageSecurityLevel2").innerHTML = "Security Level: " + level;
            if (data.Image != null)
                document.getElementById("LayoutPageProfilePic2").src = data.Image;

            document.getElementById("LayoutPageFullName3").innerHTML = data.FullName;
            if (data.Image != null)
                document.getElementById("LayoutPageProfilePic3").src = data.Image;
        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });
});
function isEmptyOrSpaces(str) {
    return str === null || str.match(/^ *$/) !== null;
} 