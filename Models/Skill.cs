using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace skillsight.API.Models;

// Specifies the database table name for the model
[Table("skills")]
[Index("TypeId", Name = "type_id")]
public partial class Skill
{
    // Id as the primary key of the table
    [Key]
    [Column("id")]
    public int Id { get; set; }

    // Skill name field
    [Column("skill_name")]
    [StringLength(255)]
    public required string SkillName { get; set; }

    // Foreign key relationship to the SkillType model
    [Column("type_id")]
    public int TypeId { get; set; }

    // Collection navigation property for RoleSkills related to the Skill
    [InverseProperty("Skill")]
    public virtual ICollection<RoleSkill> RoleSkills { get; } = new List<RoleSkill>();

    // Navigation property to the SkillType model
    [ForeignKey("TypeId")]
    [InverseProperty("Skills")]
    public virtual required SkillType Type { get; set; }

    // Collection navigation property for UserSkills related to the Skill
    [InverseProperty("Skill")]
    public virtual ICollection<UserSkill> UserSkills { get; } = new List<UserSkill>();
}
