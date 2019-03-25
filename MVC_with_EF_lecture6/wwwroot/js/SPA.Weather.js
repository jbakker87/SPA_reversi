SPA.Weather = function () {

    function getWeather() {

        var url = "https://api.openweathermap.org/data/2.5/weather?q=Zwolle&APPID=9e6c864859d333b6ba01d6ee8aa0e415";
        $.ajax(url).done(function (data) { succes(data); });

        function succes(data) {
            let result = data.weather[0].icon;
            let icon = "http://openweathermap.org/img/w/" + result + '.png';

            let celcius = (data.main.temp - 273.15).toFixed(1);
            $("nav > .container").append('<p class="mb-2 text-light">Het weer in: ' + data.name + " " + celcius + '°C </p><img src="' + icon + '">');
        }
    }

    return {
        getWeather: getWeather
    }
}();