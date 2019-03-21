function requestJson() {
    $(jsonDiv).empty();
    var url = 'https://localhost:44385/api/game';
    $.getJSON(url).done(function (data) { success(data); }).fail(function () { fail(); });
}

function success(data) {
    $.each(data, function (i, game) {
        $(jsonDiv).append('<h3>Game: ' + i + '</h3><p>p1:' + game.scorePlayer1 + ' p2:' + game.scorePlayer2 + '</p>');
    });
    //let jsonData = data[0].gameToken;

    //$(jsonDiv).append('<p>' + jsonData + '</p>');
}

function fail() {
    return SPA.showPopup('Er ging iets mis', 'Alles is naar de klote', 'rood');
}



let AddJson = function () {

    var score1 = $('#score1').val();
    var score2 = $('#score2').val();
    var game = {
        ScorePlayer1: score1,
        ScorePlayer2: score2
    }
    //alert(JSON.stringify(game));
    $.ajax({
        url: 'https://localhost:44385/api/reversi/',
        type: 'POST',
        data: JSON.stringify(game),
        contentType: 'application/json',
        success: function (newScore) {
            alert(JSON.stringify(newScore));
            $(jsonDiv).append('<p>p1:' + newScore.scorePlayer1 + ' p2:' + newScore.scorePlayer2 + ' token:' + newScore.description + '</p>');
        }
    })
};
