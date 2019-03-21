weatherWidget = function () {

    var url = "https://api.openweathermap.org/data/2.5/weather?q=Zwolle&APPID=9e6c864859d333b6ba01d6ee8aa0e415";
    $.ajax(url).done(function (data) { succes(data); });

    function succes(data) {
        let result = data.weather[0].icon;
        let icon = "";
        if (result === "01d") {
            icon = "http://openweathermap.org/img/w/01d.png";
        }
        else if (result === "01n") {
            icon = "http://openweathermap.org/img/w/01n.png";
        }
        else if (result === "02d") {
            icon = "http://openweathermap.org/img/w/02d.png";
        }
        else if (result === "02n") {
            icon = "http://openweathermap.org/img/w/02n.png";
        }
        else if (result === "03d") {
            icon = "http://openweathermap.org/img/w/03d.png";
        }
        else if (result === "03n") {
            icon = "http://openweathermap.org/img/w/03n.png";
        }
        else if (result === "04d") {
            icon = "http://openweathermap.org/img/w/04d.png";
        }
        else if (result === "04n") {
            icon = "http://openweathermap.org/img/w/04n.png";
        }
        else if (result === "09d") {
            icon = "http://openweathermap.org/img/w/09d.png";
        }
        else if (result === "09n") {
            icon = "http://openweathermap.org/img/w/09n.png";
        }
        else if (result === "10d") {
            icon = "http://openweathermap.org/img/w/10d.png";
        }
        else if (result === "10n") {
            icon = "http://openweathermap.org/img/w/10n.png";
        }
        else if (result === "11d") {
            icon = "http://openweathermap.org/img/w/11d.png";
        }
        else if (result === "11n") {
            icon = "http://openweathermap.org/img/w/11n.png";
        }
        else if (result === "13d") {
            icon = "http://openweathermap.org/img/w/13d.png";
        }
        else if (result === "13n") {
            icon = "http://openweathermap.org/img/w/13n.png";
        }
        else if (result === "50d") {
            icon = "http://openweathermap.org/img/w/50d.png";
        }
        else {
            icon = "http://openweathermap.org/img/w/50n.png";
        }
        let celcius = (data.main.temp - 273.15).toFixed(1);
        $("nav > .container").append('<p class="mb-2 text-light">Het weer in: ' + data.name + " " + celcius + '°C </p><img src="' + icon + '">');
    }
}();