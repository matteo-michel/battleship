using Grpc.Core;

namespace BattleShip.API.Service;

public class LeaderBoardGrpcService : LeaderBoardService.LeaderBoardServiceBase
{
    private readonly LeaderboardContext _context;

    public LeaderBoardGrpcService(LeaderboardContext context)
    {
        _context = context;
    }

    public override Task<LeaderBoardList> GetLeaderBoard(Empty request, ServerCallContext context)
    {
        var leaderBoards = _context.LeaderBoards
            .OrderByDescending(l => l.Score)
            .Select(l => new LeaderBoardOutput
            {
                Name = l.Name,
                Score = l.Score
            })
            .ToList();

        var leaderBoardList = new LeaderBoardList();
        leaderBoardList.LeaderBoards.AddRange(leaderBoards);

        return Task.FromResult(leaderBoardList);
    }

    public override Task<LeaderBoardOutput> AddLeaderBoard(LeaderBoard request, ServerCallContext context)
    {
        _context.LeaderBoards.Add(request);
        _context.SaveChanges();

        return Task.FromResult(new LeaderBoardOutput
        {
            Name = request.Name,
            Score = request.Score
        });
    }
}