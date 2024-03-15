using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skillsight.API.Models;

// Specifies the database table name for the model
[Table("users")]
public partial class User
{
    // Primary key of the table
    [Key]
    [Column("id")]
    public int Id { get; set; }

    // Foreign key for the JobRole
    [Column("jobrole_id")]
    [Required]
    public int JobRoleId { get; set; }

    // First name field
    [Column("firstname")]
    [StringLength(50)]
    [Required]
    public required string Firstname { get; set; }

    // Last name field
    [Column("lastname")]
    [StringLength(50)]
    [Required]
    public required string Lastname { get; set; }

    // Username field
    [Column("username")]
    [StringLength(50)]
    [Required]
    public required string Username { get; set; }

    // Email field
    [Column("email")]
    [StringLength(255)]
    [EmailAddress]
    [Required]
    public required string Email { get; set; }

    // Password field
    [Column("password")]
    [StringLength(255)]
    [Required]
    public required string Password { get; set; }

    // Navigation property for JobRole
    [ForeignKey("JobRoleId")]
    [InverseProperty("Users")]
    public virtual JobRole? JobRole { get; set; }
}
