SPA.Model = function () {

    function promise() {
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
        promise: promise
    };

}();