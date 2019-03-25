using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SPA.DAL;
using SPA.Models;

namespace SPA.Controllers
{
    public class GamesController : Controller
    {

        public Player Player { get; set; }
        private readonly GameContext _context;

        public GamesController(GameContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            Player currentPlayer = await _context.Player.FirstOrDefaultAsync(i => i.PlayerId == Convert.ToInt32(User.Identity.Name));

            return View(await _context.Game
                .Include(i => i.Players).ToListAsync());
        }

        // GET: Games
        [ActionName("Active")]
        public async Task<IActionResult> Active(int? id)
        {

            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.GameId == id);

            return View(game);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Current(int? id)
        {
            Game game = null;
            if (id == null)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                game = await _context.Game
                    .FirstOrDefaultAsync(m => m.GameId == id);
                if (game == null)
                {
                    return NotFound();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,GameToken,Description,Turn,PlayerWhiteId,PlayerBlackId")] Game game)
        {
            if (ModelState.IsValid)
            {
                Player currentPlayer = await _context.Player
                    .Include(g => g.Game)
                    .FirstOrDefaultAsync(m => m.PlayerId == Convert.ToInt32(User.Identity.Name));

                game.GameToken = Guid.NewGuid().ToString();

                game.Players.Add(currentPlayer);
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int? id)
        {

            if(id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                Player joiningPlayer = await _context.Player
                .FirstOrDefaultAsync(m => m.PlayerId == Convert.ToInt32(User.Identity.Name));

                Game gameToBeJoined = await _context.Game
                    .Include(p => p.Players)
                    .FirstOrDefaultAsync(i => i.GameId == id);

                gameToBeJoined.Players.Add(joiningPlayer);
                _context.Add(gameToBeJoined);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,GameToken,Description,Turn,PlayerWhiteId,PlayerBlackId")] Game game)
        {
            if (id != game.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.GameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.FindAsync(id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.GameId == id);
        }

        [HttpGet]
        public IActionResult Active()
        {
            return View();
        }
    }
}
