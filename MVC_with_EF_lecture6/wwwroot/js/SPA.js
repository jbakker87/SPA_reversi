let SPA = (function () {

        //function init() {
        //    SPA.Data.init('productie');
        //}

    function feedback(title, msg, color) {
        SPA.Feedback.show(title, msg, color);
    }

    function drawBoard(id) {
        SPA.Reversi.drawBoard(id);
    }

    function getSpellen() {
        return SPA.data.getSpellen();
    }

    function getWeather() {
        SPA.Weather.getWeather();
    }

    return {
        //init: init,
        feedback: feedback,
        getSpellen: getSpellen,
        drawBoard: drawBoard,
    };

})();