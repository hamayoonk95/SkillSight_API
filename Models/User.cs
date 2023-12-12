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

    // First name field
    [Column("firstname")]
    [StringLength(50)]
    public required string Firstname { get; set; }

    // Last name field
    [Column("lastname")]
    [StringLength(50)]
    public required string Lastname { get; set; }

    // Username field
    [Column("username")]
    [StringLength(50)]
    public required string Username { get; set; }

    // Email field
    [Column("email")]
    [StringLength(255)]
    public required string Email { get; set; }

    // Password field
    [Column("password")]
    [StringLength(255)]
    public required string Password { get; set; }

    // Collection navigation property for Projects related to the User
    [InverseProperty("User")]
    public virtual ICollection<Project> Projects { get; } = new List<Project>();

    // Collection navigation property for UserSkills related to the User
    [InverseProperty("User")]
    public virtual ICollection<UserSkill> UserSkills { get; } = new List<UserSkill>();
}
