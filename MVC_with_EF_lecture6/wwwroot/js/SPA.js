let SPA = (function () {

    function init() {
        SPA.data.init('productie');
    }

    //function showPopup(title, msg, color) {
    //    return data.showPopup(title, msg, color);
    //}

    function showPopup(title, message, color) {
        var dialogColor = "#dialog-" + color;
        var el1 = document.createElement("div");
        var el2 = document.createElement("div");
        el1.appendChild(el2);
        el1.setAttribute("id", "dialog-" + color);
        el2.setAttribute("id", "icon3");

        if (color === "groen") {
            icon = "<span class='glyphicon glyphicon-ok'></span>";
            menuColor = "#00C851";
        }
        else {
            icon = "<span class='glyphicon glyphicon-exclamation-sign'></span>";
            menuColor = "#ff4444";
        }

        document.body.appendChild(el1);


        $("#icon3").append(icon + message);

        $(dialogColor).dialog({
            autoOpen: false,
            modal: true,
            title: title,
            height: 200,
            width: 400,
            buttons: {
                "OK ": function () {
                    $(dialogColor).dialog("close");
                }
            },
            close: function () {
                $("#icon3").empty();
            }
        }).prev(".ui-dialog-titlebar").css("background-color", menuColor);

        $(dialogColor).dialog("open");

    }

    function getSpellen() {
        return SPA.data.getSpellen();
    }

    return {
        init: init,
        getSpellen: getSpellen,
        showPopup: showPopup
    };

})();