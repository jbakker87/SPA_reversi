//let RequestJson = function () {

//    let jsonDiv = document.getElementById("jsonDiv");

//    $(jsonDiv).empty();
//    var url = 'https://localhost:44396/api/Reversi';
//    $.ajax(url).done(function (data) { success(data); });

//    function success(data) {
//        alert(data[0].playerName);
//    }
//}

//let Validate = function () {
//    $.ajax(
//        {
//            type: "POST",
//            url: url: '@Url.Action("Validate", "Account")',
//            data: {
//                username: $('#username').val(),
//                password: $('#password').val()
//            },
//            error: function (result) {
//                alert("There is a Problem, Try Again!");
//            },
//            success: function (result) {
//                console.log(result);
//                if (result.status == true) {
//                    window.location.href = '@Url.Action("Index", "Home")';
//                }
//                else {
//                    alert(result.message);
//                }
//            }
//        });
//}