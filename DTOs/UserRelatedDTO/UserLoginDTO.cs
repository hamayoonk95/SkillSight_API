using System.ComponentModel.DataAnnotations;

public class UserLoginDTO
{
    [Required]
    [StringLength(50)]
    public required string Username { get; set; }
    [Required]
    [StringLength(255, MinimumLength = 8)]
    public required string Password { get; set; }
}
