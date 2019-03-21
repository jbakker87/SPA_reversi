let popUp_Widget = (function () {

    function show(title, message, color) {

        if (color === "grijs") {
            bootstrap_color = "#4B515D";
            _showGrey(title, message, color, bootstrap_color);
        }
        else if (color === "groen") {
            bootstrap_color = "#00C851";
            _showGreen(title, message, color, bootstrap_color);
        }
        else
        {
            bootstrap_color = "#ff4444";
            _showRed(title, message, color, bootstrap_color);

        }
    };

    function _showGrey(title, message, color, bootstrap_color) {

        var dialogColor = "#dialog-" + color;
        var el1 = document.createElement("div");
        var el2 = document.createElement("div");
        el1.appendChild(el2);
        el1.setAttribute("id", "dialog-" + color);
        el2.setAttribute("id", "icon3");

        var icon = "<span class='glyphicon glyphicon-user'></span>";
        document.body.appendChild(el1);


        $("#icon3").append(icon + message);

        $(dialogColor).dialog({
            autoOpen: false,
            modal: true,
            title: title,
            height: 200,
            width: 400,
            buttons: {
                "Akkoord": function () {
                    _storeMsg("naam kreeg akkoord");
                    //print();
                    $(dialogColor).dialog("close");
                    popUp_Widget.showWhat('Gelukt!', 'Naam is toegevoegd aan het spel.', 'groen');
                },
                "Weigeren": function () {
                    _storeMsg("naam werd geweigerd");
  
                  //print();
                    //$(this).dialog("close");
                    $(dialogColor).dialog("close");
                    popUp_Widget.showWhat('Helaas!', 'Naam is geweigerd voor het spel.', 'rood');
                }
            },
            close: function () {
                $("#icon3").empty();
            }
        }).prev(".ui-dialog-titlebar").css("background-color", bootstrap_color);

        $(dialogColor).dialog("open");
    };

    function _showGreen(title, message, color, bootstrap_color) {

        var dialogColor = "#dialog-" + color;
        var el1 = document.createElement("div");
        var el2 = document.createElement("div");
        el1.appendChild(el2);
        el1.setAttribute("id", "dialog-" + color);
        el2.setAttribute("id", "icon1");

        var icon = "<span class='glyphicon glyphicon-ok'></span>";
        document.body.appendChild(el1);


        $("#icon1").append(icon + message);

        $(dialogColor).dialog({
            autoOpen: false,
            modal: true,
            title: title,
            height: 200,
            width: 400,
            buttons: {
                "Sluiten": function () {
                    $(dialogColor).dialog("close");
                }
            },
            close: function () {
                $("#icon1").empty();
            }
        }).prev(".ui-dialog-titlebar").css("background-color", bootstrap_color);

        $(dialogColor).dialog("open");
    };

    function _showRed(title, message, color, bootstrap_color) {

        var dialogColor = "#dialog-" + color;
        var el1 = document.createElement("div");
        var el2 = document.createElement("div");
        el1.appendChild(el2);
        el1.setAttribute("id", "dialog-" + color);
        el2.setAttribute("id", "icon2");

        var icon = "<span class='glyphicon glyphicon-exclamation-sign'></span>";
        document.body.appendChild(el1);

        $("#icon2").append(icon + message);


        $(dialogColor).dialog({
            autoOpen: false,
            title: title,
            modal: true,
            height: 200,
            width: 400,
            buttons: {
                "Sluiten": function () {

                    _storeMsg("Bij naam ging iets mis");
                    //print();
                    $(dialogColor).dialog("close");
                }
            },
            close: function () {
                $("#icon2").empty();
            }
        }).prev(".ui-dialog-titlebar").css("background-color", bootstrap_color);
        $(dialogColor).dialog("open");
    };


    function _storeMsg(message) {
        if (sessionStorage.msg) {
            let msgArray = JSON.parse(sessionStorage.msg);


            if (msgArray.length < 10) {
                msgArray.push(message);
                sessionStorage.msg = JSON.stringify(msgArray);
            }

        }
        else {
            sessionStorage.msg = JSON.stringify([message]);
        }
    };

    function feedback () {

        var temp = $("#textbox1").val();
        //alert(temp);
        if (temp === "groen") {
            popUp_Widget.showWhat('Nieuwe deelnemer', 'Naam wilt zich aanmelden voor jouw spel. Geef akkoord.', 'groen');
        }
        else if (temp === "rood") {
            popUp_Widget.showWhat('Er ging iets mis!', 'Je gegevens konden niet worden opgehaald. Dat is balen! Probeer het opnieuw.', 'rood');
        }
        else {
            alert("Onjuiste invoer. Voer in rood of groen");
        }
    };

    return {
        showWhat: show,
        feedback: feedback,
        msg: _storeMsg
    };

})();


function print() {
    var div1 = document.createElement("h1");
    for (var i = 0; i < sessionStorage.length; i++) {
        document.body.append(div1);
        div1.append(sessionStorage.msg);
    }
};
