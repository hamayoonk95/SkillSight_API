using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace skillsight.API.Models;

[Table("skill_types")]
public partial class SkillType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type_name")]
    [StringLength(50)]
    public required string TypeName { get; set; }

    [InverseProperty("Type")]
    public virtual ICollection<Skill> Skills { get; } = new List<Skill>();
}
