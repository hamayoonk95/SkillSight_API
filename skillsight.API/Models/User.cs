using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skillsight.API.Models;

[Table("users")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("firstname")]
    [StringLength(50)]
    public required string Firstname { get; set; }

    [Column("lastname")]
    [StringLength(50)]
    public required string Lastname { get; set; }

    [Column("username")]
    [StringLength(50)]
    public required string Username { get; set; }

    [Column("email")]
    [StringLength(255)]
    public required string Email { get; set; }

    [Column("password")]
    [StringLength(255)]
    public required string Password { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Project> Projects { get; } = new List<Project>();

    [InverseProperty("User")]
    public virtual ICollection<UserSkill> UserSkills { get; } = new List<UserSkill>();
}
