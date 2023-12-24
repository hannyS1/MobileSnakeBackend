namespace MobileSnake.Api.Contracts.Data;

public class ResultDto
{
    public int Id { get; set; }
    
    public int Score { get; set; }

    public DateTime CreationDateTime { get; set; }
    
    public UserDto User { get; set; }
}