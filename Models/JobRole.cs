using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skillsight.API.Models;

// Specifies the database table name for the model
[Table("job_roles")]
public partial class JobRole
{
    // Id as the primary key of the table
    [Key]
    [Column("id")]
    public int Id { get; set; }

    // Role title field
    [Column("role_title")]
    [StringLength(50)]
    public required string RoleTitle { get; set; }

    // Collection navigation property for JobPostings related to the JobRole
    [InverseProperty("Role")]
    public virtual ICollection<JobPosting> JobPostings { get; } = new List<JobPosting>();

    // Collection navigation property for Projects related to the JobRole
    [InverseProperty("Role")]
    public virtual ICollection<Project> Projects { get; } = new List<Project>();

    // Collection navigation property for RoleSkills related to the JobRole
    [InverseProperty("Role")]
    public virtual ICollection<RoleSkill> RoleSkills { get; } = new List<RoleSkill>();
}
