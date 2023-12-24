using MobileSnake.Api.Contracts.Data;
using MobileSnake.Entities;

namespace MobileSnake.AppServices.Mappers.Interfaces;

public interface IResultMapper
{
    public ResultDto Map(Result result);
}