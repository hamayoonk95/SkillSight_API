using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace skillsight.API.Models;

[Table("user_skills")]
[Index("SkillId", Name = "skill_id")]
[Index("UserId", Name = "user_id")]
public partial class UserSkill
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("skill_id")]
    public int SkillId { get; set; }

    [ForeignKey("SkillId")]
    [InverseProperty("UserSkills")]
    public virtual required Skill Skill { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserSkills")]
    public virtual required User User { get; set; }
}
