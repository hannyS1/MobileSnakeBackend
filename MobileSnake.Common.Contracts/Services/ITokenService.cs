namespace MobileSnake.Common.Contracts.Services;

public interface ITokenService
{
    public string CreateToken(int userId);
    
    public int? GetUserIdByToken(string token);
}