using Microsoft.EntityFrameworkCore;
using SPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA.DAL
{
    public class GameContext : DbContext
    {

        public GameContext(DbContextOptions<GameContext> options) : base(options) { }

        public DbSet<Game> Game { get; set; }
        public DbSet<Player> Player { get; set; }
    }
}
