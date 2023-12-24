using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileSnake.Entities;

public class Result
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("score")]
    public int Score { get; set; }
    
    [Column("creation_date_time")]
    [DataType(DataType.Date)]
    public DateTime CreationDateTime { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    public User User { get; set; }
}