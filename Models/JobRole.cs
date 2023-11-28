using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace skillsight.API.Models;

[Table("job_roles")]
public partial class JobRole
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_title")]
    [StringLength(50)]
    public required string RoleTitle { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<JobPosting> JobPostings { get; } = new List<JobPosting>();

    [InverseProperty("Role")]
    public virtual ICollection<Project> Projects { get; } = new List<Project>();

    [InverseProperty("Role")]
    public virtual ICollection<RoleSkill> RoleSkills { get; } = new List<RoleSkill>();
}
