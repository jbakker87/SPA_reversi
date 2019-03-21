let SPA = (function () {


    drawBoard = function () {

        var boardInfo = {
            score: null,
            rows: 8,
            cols: 8
        };

        var table = $("<table></table>");

        for (var i = 0; i < boardInfo.rows; i++) {

            var trow = $("<tr class='tableRow'></tr>");

            for (var j = 0; j < boardInfo.cols; j++) {

                var tcell = $("<td class='tdBoard' xpos='" + j + "' ypos='" + i + "'></td>");

                $(tcell).on("click", function (obj) {

                    tcellClick(obj);
                });

                $(trow).append(tcell);
            }

            $(table).append(trow);
        }
        $('#tableDiv').append(table);
    }
})();

function tcellClick(obj) {
    xpos = obj.target.getAttribute("xpos");
    ypos = obj.target.getAttribute("ypos");
    $(obj.target).append("<span class='stone'><span>");
}

$(document).ready(function() {

   drawBoard();

});