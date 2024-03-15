using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace skillsight.API.Models;

// Specifies the database table name for the model
[Table("job_postings")]
// Index attribute creates an index on the 'RoleId' column for faster queries
[Index("RoleId", Name = "role_id")]
public partial class JobPosting
{
    // Id as the primary key of the table
    [Key]
    [Column("id")]
    public int Id { get; set; }

    // Role ID field
    [Column("role_id")]
    public int RoleId { get; set; }

    // Description field
    [Column("job_description", TypeName = "mediumtext")]
    public string? JobDescription { get; set; }

    // Job Title field
    [Column("job_title")]
    [StringLength(255)]
    public string? JobTitle { get; set; }

    // Company name field
    [Column("company_name")]
    [StringLength(255)]
    public string? CompanyName { get; set; }

    // Date job scraped field 
    [Column("date_scraped", TypeName = "date")]
    public DateTime DateScraped { get; set; }

    // ForeignKey attribute links 'RoleId' to the JobRole model
    [ForeignKey("RoleId")]
    [InverseProperty("JobPostings")]
    [JsonIgnore]
    public virtual JobRole? Role { get; set; }
}
