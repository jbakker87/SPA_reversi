SPA.Data = (function () {

    var configMap = {

        environment: 'development'
    }

    var endpoint = {
        url: "https://localhost:44338/",
        get: "api/chuck"
    }

    function checkConfig() {
        $('#configTxt').append(configMap.environment);
        return alert(configMap.environment);
    }

    function init(environment) {
        configMap.environment = environment;
    }

    function apiPopup(title, msg, color) {
        return SPA.showPopup(title, msg, color);
    }

    function getSpellen() {
        //if (confiMap.environment === "productie") {
        var min = 0;
        var max = 2;
        var ranomInt = Math.floor(Math.random() * (max - min + 1)) + min;
        return new Promise(function (resolve, reject) {

            $.ajax(endpoint.url + endpoint.get)
                .done(function (data) {
                    resolve(apiPopup('Gelukt', data[ranomInt].text, 'groen'));
                })
                .fail(function () {
                    reject(apiPopup('Mislukt', 'Je gegevens konden niet worden opgehaald. Dat is balen! Probeer het opnieuw.', 'rood'));
                })
        });

        //return new Promise(function (resolve, reject) {
        //    $.ajax(endpoint.url + endpoint.get)
        //        .done(resolve(apiPopup('Gelukt!', "data: " + data, "groen"))
        //            .fail(reject(apiPopup('Mislukt', "data: " + data, "rood"))

    }

    function showPopup(title, message, color) {
        alert("werkt");
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

    return {
        init: init,
        getSpellen: getSpellen,
        config: configMap,
        checkConfig: checkConfig,
    };

})();