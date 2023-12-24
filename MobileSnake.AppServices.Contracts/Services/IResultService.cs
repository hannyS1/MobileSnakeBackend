using MobileSnake.Api.Contracts.Data;

namespace MobileSnake.AppServices.Contracts.Services;

public interface IResultService
{
    Task<List<ResultDto>> GetRecords();
    
    Task<List<ResultDto>> GetByUserId(int userId);

    Task<ResultDto> Create(CreateResultDto dto, int userId);
}