using BattleShip.Models;
using BattleShip.Models.DTO.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BattleShip.API;

[Route("leaderboard")]
[ApiController]
public class LeaderBoardController : Controller
{
    
    [HttpGet]
    [Authorize]
    public JsonHttpResult<List<LeaderBoardOutput>> GetLeaderBoard(LeaderboardContext context)
    {
        var leaderBoards = context.LeaderBoards
            .OrderByDescending(l => l.Score)
            .Select(l => new LeaderBoardOutput
            {
                Name = l.Name,
                Score = l.Score
            })
            .ToList();
        
        return TypedResults.Json(leaderBoards);
    }
    
    [HttpPost]
    [Authorize]
    public JsonHttpResult<LeaderBoardOutput> AddToLeaderBoard(LeaderboardContext context, LeaderBoard leaderBoard)
    {
        context.LeaderBoards.Add(leaderBoard);
        context.SaveChanges();
        
        return TypedResults.Json(new LeaderBoardOutput
        {
            Name = leaderBoard.Name,
            Score = leaderBoard.Score
        });
    }
}