SPA.Reversi = (function () {

    var GameId;

    function drawBoard(id) {
        GameId = id;

        $('#tableDiv').empty();

        var url = 'https://localhost:44385/api/game/' + id;
        $.getJSON(url).done(function (data) { success(data); }).fail(function () { fail(); });

        function success(data) {

            var result = JSON.stringify(data);
            result = JSON.parse(result);

            var boardInfo = {
                rows: result.board.length,
                cols: result.board.length
            };

            var table = $("<table></table>");

            for (var i = 0; i < boardInfo.rows; i++) {

                var trow = $("<tr class='tableRow'></tr>");

                for (var j = 0; j < boardInfo.cols; j++) {

                    var tcell = $("<td class='tdBoard' xpos='" + i + "' ypos='" + j + "'></td>");

                    if (result.board[i][j] == 1) $(tcell).append("<span class='stoneWhite'><span>")
                    else if (result.board[i][j] == 2) $(tcell).append("<span class='stoneBlack'><span>")
                    $(tcell).on("click", function (obj) {
                        tcellClick(obj, GameId);
                    });

                    $(trow).append(tcell);
                }

                $(table).append(trow);
            }
            $('#tableDiv').append(table);
        }

        function tcellClick(obj) {
            xpos = obj.target.getAttribute("xpos");
            ypos = obj.target.getAttribute("ypos");

            var game = {
                x: xpos,
                y: ypos
            }

            $.ajax({
                url: 'https://localhost:44385/api/game/turn/' + GameId,
                type: 'PUT',
                data: JSON.stringify(game),
                contentType: 'application/json',
                statusCode: {
                    400: function () {
                        SPA.feedback("Something went wrong", "This move is not valid", "rood");
                    },
                    success: function () {
                        $(obj.target).append("<span class='stoneBlack'><span>");
                    },
                }
            });

            
        }
    }



    return {
        drawBoard: drawBoard,
    };

})();