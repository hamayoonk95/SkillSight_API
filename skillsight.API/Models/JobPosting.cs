using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace skillsight.API.Models;

[Table("job_postings")]
[Index("RoleId", Name = "role_id")]
public partial class JobPosting
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("job_description", TypeName = "mediumtext")]
    public string? JobDescription { get; set; }

    [Column("job_title")]
    [StringLength(255)]
    public string? JobTitle { get; set; }

    [Column("company_name")]
    [StringLength(255)]
    public string? CompanyName { get; set; }

    [Column("date_scraped", TypeName = "datetime")]
    public DateTime DateScraped { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("JobPostings")]
    [JsonIgnore]
    public virtual JobRole? Role { get; set; }
}
