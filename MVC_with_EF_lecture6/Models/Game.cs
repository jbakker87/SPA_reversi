using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SPA.Models
{
    public class Game
    {


        public Game()
        {
            Board = new int[8, 8]; Board[3, 3] = 1; Board[3, 4] = 2; Board[4, 3] = 2; Board[4, 4] = 1;
            GameToken = Guid.NewGuid().ToString();
        }

        [Key]
        [DisplayName("GameId")]
        public int GameId { get; set; }

        [NotMapped]
        public int ScorePlayer1 { get; set; }

        [NotMapped]
        public int ScorePlayer2 { get; set; }

        public string GameToken { get; set; }
        public string Description { get; set; }
        public int Turn { get; set; }
        public List<Player> Players { get; set; } = new List<Player>(); 

        [DisplayName("Player 1")]
        [ForeignKey("PlayerId")]
        public int PlayerWhiteId { get; set; }

        [DisplayName("Player 2")]
        [ForeignKey("PlayerId")]
        public int PlayerBlackId { get; set; }

        [NotMapped]
        public int[,] Board { get; set; }

    }
}
