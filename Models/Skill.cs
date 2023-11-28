using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace skillsight.API.Models;

[Table("skills")]
[Index("TypeId", Name = "type_id")]
public partial class Skill
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("skill_name")]
    [StringLength(255)]
    public required string SkillName { get; set; }

    [Column("type_id")]
    public int TypeId { get; set; }

    [InverseProperty("Skill")]
    public virtual ICollection<RoleSkill> RoleSkills { get; } = new List<RoleSkill>();

    [ForeignKey("TypeId")]
    [InverseProperty("Skills")]
    public virtual required SkillType Type { get; set; }

    [InverseProperty("Skill")]
    public virtual ICollection<UserSkill> UserSkills { get; } = new List<UserSkill>();
}
