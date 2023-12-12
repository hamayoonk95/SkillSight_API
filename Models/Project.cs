using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace skillsight.API.Models;

// Specifies the database table name for the model
[Table("projects")]
// Indexes for efficient querying by RoleId and UserId
[Index("RoleId", Name = "role_id")]
[Index("UserId", Name = "user_id")]
public partial class Project
{
    // Primary key of the table
    [Key]
    [Column("id")]
    public int Id { get; set; }

    // Title of the project
    [Column("title")]
    [StringLength(50)]
    public required string Title { get; set; }

    // Description of the project
    [Column("description", TypeName = "text")]
    public required string Description { get; set; }

    // Foreign key relationship to the User model
    [Column("user_id")]
    public int UserId { get; set; }

    // Foreign key relationship to the JobRole model
    [Column("role_id")]
    public int RoleId { get; set; }

    // Navigation property to the JobRole model
    [ForeignKey("RoleId")]
    [InverseProperty("Projects")]
    public virtual required JobRole Role { get; set; }

    // Navigation property to the User model
    [ForeignKey("UserId")]
    [InverseProperty("Projects")]
    public virtual required User User { get; set; }
}
