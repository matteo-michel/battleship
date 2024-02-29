using BattleShip.Models;
using Microsoft.EntityFrameworkCore;

namespace BattleShip.API;

public class LeaderboardContext : DbContext
{
    public DbSet<LeaderBoard> LeaderBoards { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=leaderboard.db");
    }
}