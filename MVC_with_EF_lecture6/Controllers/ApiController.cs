using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SPA.DAL;
using SPA.Models;

namespace SPA.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly GameContext _context;

        public ApiController(GameContext context)
        {
            _context = context;
        }

        // GET: api/game
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            return await _context.Game.ToListAsync();
        }

        // GET: api/game/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Game.FindAsync(id);
            game.Board = JsonConvert.DeserializeObject<int[,]>(game.boardString);
            
            if (game == null)
            {
                return NotFound();
            }
            return game;
        }   

        //// POST: api/game
        //[HttpPost]
        //public async Task<ActionResult<Game>> PostGame(Game game)
        //{
        //    _context.Game.Add(game);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetGame", new { id = game.GameId }, game);
        //}


        // POST: api/game/turn/id
        [HttpPut("turn/{id}")]
        public async Task<ActionResult<Game>> PutGame(int? id, Turn turn)
        {
            Game game = _context.Game.FirstOrDefault(i => i.GameId == id);
            game.Board = JsonConvert.DeserializeObject<int[,]>(game.boardString);
            game.Board[turn.X, turn.Y] = 1;

            game.boardString = JsonConvert.SerializeObject(game.Board);



            _context.Game.Update(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/game/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> DeleteGame(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return game;
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.GameId == id);
        }
    }
}
