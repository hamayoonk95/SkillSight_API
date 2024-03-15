using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace skillsight.API.Models;

// Specifies the database table name for the model
[Table("role_skills")]
// Indexes for efficient querying by RoleId and SkillId
[Index("RoleId", Name = "role_id")]
[Index("SkillId", Name = "skill_id")]
public partial class RoleSkill
{
    // Primary key of the table
    [Key]
    [Column("id")]
    public int Id { get; set; }

    // Foreign key relationship to the JobRole model
    [Column("role_id")]
    public int RoleId { get; set; }

    // Foreign key relationship to the Skill model
    [Column("skill_id")]
    public int SkillId { get; set; }

    // Frequency of the skill in relation to the role
    [Column("frequency")]
    public int Frequency { get; set; }

    // Date when skill is scraped
    [Column("date_scraped", TypeName = "date")]
    public DateTime DateScraped { get; set; }

    // Navigation property to the JobRole model
    [ForeignKey("RoleId")]
    [InverseProperty("RoleSkills")]
    public virtual required JobRole Role { get; set; }

    // Navigation property to the Skill model
    [ForeignKey("SkillId")]
    [InverseProperty("RoleSkills")]
    public virtual required Skill Skill { get; set; }
}
