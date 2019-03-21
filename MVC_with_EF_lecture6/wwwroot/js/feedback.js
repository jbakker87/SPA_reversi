var modalConfirm = function (callback) {

    $("#btn-confirm").on("click", function () {
        $("#mi-modal").modal('show');
    });

    $("#modal-btn-si").on("click", function () {
        callback(true);
        $("#mi-modal").modal('hide');
    });

    $("#modal-btn-no").on("click", function () {
        callback(false);
        $("#mi-modal").modal('hide');
    });
};

modalConfirm(function (confirm) {
    if (confirm) {
        //Acciones si el usuario confirma
        $("#result").html("CONFIRMADO");
    } else {
        //Acciones si el usuario no confirma
        $("#result").html("NO CONFIRMADO");
    }
});












////Module Data is verantwoordelijk voor de API communicatie

//SPA.feedback = (function () {

//    function popup(title, message, color) {

//        var dialogColor = "#dialog-" + color;
//        var el1 = document.createElement("div");
//        var el2 = document.createElement("div");
//        el1.appendChild(el2);
//        el1.setAttribute("id", "dialog-" + color);
//        el2.setAttribute("id", "icon3");

//        if (color === "groen") {
//            icon = "<span class='glyphicon glyphicon-ok'></span>";
//            menuColor = "#00C851";
//        }
//        else {
//            icon = "<span class='glyphicon glyphicon-exclamation-sign'></span>";
//            menuColor = "#ff4444";
//        }

//        document.body.appendChild(el1);


//        $("#icon3").append(icon + message);

//        $(dialogColor).dialog({
//            autoOpen: false,
//            modal: true,
//            title: title,
//            height: 200,
//            width: 400,
//            buttons: {
//                "OK ": function () {
//                    $(dialogColor).dialog("close");
//                }
//            },
//            close: function () {
//                $("#icon3").empty();
//            }
//        }).prev(".ui-dialog-titlebar").css("background-color", menuColor);

//        $(dialogColor).dialog("open");

//    }

//    return {
//        popup: popup
//    };

//})();