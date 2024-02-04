using System.ComponentModel.DataAnnotations;

public class UserRegistrationDTO
{
    [Required]
    [StringLength(50)]
    public required string Firstname { get; set; }
    [Required]
    [StringLength(50)]
    public required string Lastname { get; set; }
    [Required]
    [StringLength(50)]
    public required string Username { get; set; }
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public required string Email { get; set; }
    [Required]
    [StringLength(255, MinimumLength = 8)]
    public required string Password { get; set; }
    [Required]
    public required int JobRoleId { get; set; }
}
