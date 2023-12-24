using Microsoft.EntityFrameworkCore;
using MobileSnake.Api.Contracts.Data;
using MobileSnake.AppServices.Contracts.Services;
using MobileSnake.AppServices.Mappers.Interfaces;
using MobileSnake.Database;
using MobileSnake.Entities;

namespace MobileSnake.AppServices.Services;

internal class ResultService : IResultService
{
    private readonly ApplicationContext _dbContext;
    private readonly IResultMapper _resultMapper;
    
    public ResultService(ApplicationContext dbContext, IResultMapper resultMapper)
    {
        _dbContext = dbContext;
        _resultMapper = resultMapper ?? throw new ArgumentNullException(nameof(resultMapper));
    }

    public async Task<List<ResultDto>> GetRecords()
    {
        // return await _dbContext.Database.SqlQueryRaw<Result>(
        //     "select r.id as result_id, r.user_id, u.name, max(r.score) as max_score " +
        //     "from Results r left join Users u on r.user_id = u.id group by user_id").ToListAsync();

        // var results = await _dbContext.Results.Include(r => r.User).GroupBy(r => r.User).Select(group => new Result
        // {
        //     Id = group.First().Id,
        //     CreationDateTime = group.First().CreationDateTime,
        //     Score = group.Max(r => r.Score),
        //     UserId = group.First().UserId,
        //     User = new User
        //     {
        //         Id = group.Key.Id,
        //         Name = group.Key.Name
        //     }
        // }).ToListAsync();
        
        var groups = await _dbContext.Results.Include(r => r.User).GroupBy(r => r.User).ToListAsync();
        var results = new List<Result>();
        foreach (var group in groups)
        {
            var maxScoreResult = group.MaxBy(g => g.Score);
            results.Add(new Result
            {
                Id = maxScoreResult.Id, 
                CreationDateTime = maxScoreResult.CreationDateTime,
                Score = maxScoreResult.Score,
                User = group.Key,
                UserId = group.Key.Id,
            });
        }
        
        return results.Select(r => _resultMapper.Map(r)).ToList();
    }

    public async Task<List<ResultDto>> GetByUserId(int userId)
    {
        var results = await _dbContext.Results.Include(r => r.User).Where(r => r.UserId == userId).ToListAsync();
        return results.Select(r => _resultMapper.Map(r)).ToList();
    }

    public async Task<ResultDto> Create(CreateResultDto dto, int userId)
    {
        var result = new Result
        {
            CreationDateTime = DateTime.UtcNow,
            Score = dto.Score,
            UserId = userId,
        };
        var resultEntry = _dbContext.Results.Add(result);
        await _dbContext.SaveChangesAsync();
        return _resultMapper.Map(resultEntry.Entity);
    }
}