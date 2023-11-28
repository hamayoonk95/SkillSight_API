using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace skillsight.API.Models;

[Table("projects")]
[Index("RoleId", Name = "role_id")]
[Index("UserId", Name = "user_id")]
public partial class Project
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(50)]
    public required string Title { get; set; }

    [Column("description", TypeName = "text")]
    public required string Description { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Projects")]
    public virtual required JobRole Role { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Projects")]
    public virtual required User User { get; set; }
}
