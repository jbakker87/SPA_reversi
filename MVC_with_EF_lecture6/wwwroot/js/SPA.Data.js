SPA.Data = (function () {

    var configMap = {

        environment: 'development'
    }

    var endpoint = {
        url: "https://localhost:44385/api/game"
    }

    function checkConfig() {
        $('#configTxt').append(configMap.environment);
        return alert(configMap.environment);
    }

    function init(environment) {
        configMap.environment = environment;
    }


    function getSpellen() {
        return new Promise(function (resolve, reject) {

            $.ajax(endpoint.url + endpoint.get)
                .done(function (data) {
                    resolve(SPA.feedback('Gelukt', JSON.stringify(data), 'groen'));
                })
                .fail(function () {
                    reject(SPA.feedback('Mislukt', 'Je gegevens konden niet worden opgehaald. Dat is balen! Probeer het opnieuw.', 'rood'));
                })
        });
    }

    return {
        init: init,
        getSpellen: getSpellen,
        config: configMap,
        checkConfig: checkConfig,
    };

})();