using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace skillsight.API.Models;

[Table("role_skills")]
[Index("RoleId", Name = "role_id")]
[Index("SkillId", Name = "skill_id")]
public partial class RoleSkill
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("skill_id")]
    public int SkillId { get; set; }

    [Column("frequency")]
    public int Frequency { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("RoleSkills")]
    public virtual required JobRole Role { get; set; }

    [ForeignKey("SkillId")]
    [InverseProperty("RoleSkills")]
    public virtual required Skill Skill { get; set; }
}
