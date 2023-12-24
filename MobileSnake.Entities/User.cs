using System.ComponentModel.DataAnnotations.Schema;

namespace MobileSnake.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("password")]
    public string Password { get; set; }
}