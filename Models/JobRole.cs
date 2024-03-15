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

    [Column("details", TypeName = "json")]
    public required string Details { get; set; }

    // Collection navigation property for JobPostings related to the JobRole
    [InverseProperty("Role")]
    public virtual required ICollection<JobPosting> JobPostings { get; set; }

    // Collection navigation property for Users related to the JobRole
    [InverseProperty("JobRole")]
    public required virtual ICollection<User> Users { get; set; }

    // Collection navigation property for RoleSkills related to the JobRole
    [InverseProperty("Role")]
    public virtual ICollection<RoleSkill> RoleSkills { get; } = new List<RoleSkill>();
}
