using Microsoft.EntityFrameworkCore;
using skillsight.API.Models;

namespace skillsight.API.Data;

/******* 
    The ApplicationDbContext class, derived from DbContext, 
    Represents a session with the database, allowing for querying and saving data.
*******/
public partial class ApplicationDbContext : DbContext
{
    // Default constructor.
    public ApplicationDbContext()
    {
    }

    // Constructor accepting DbContextOptions, used for dependency injection.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet properties representing tables in the database.
    // Efmigrationshistories table for Entity Framework migrations history.
    // public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    // JobPostings table holding data about job postings
    public virtual DbSet<JobPosting> JobPostings { get; set; }

    // JobRoles table containing information about different job roles.
    public virtual DbSet<JobRole> JobRoles { get; set; }

    // Projects table storing project-related data.
    public virtual DbSet<Project> Projects { get; set; }

    // RoleSkills table mapping skills to job roles.
    public virtual DbSet<RoleSkill> RoleSkills { get; set; }

    // Skills table listing various skills.
    public virtual DbSet<Skill> Skills { get; set; }

    // SkillTypes table categorizing skills into different types.
    public virtual DbSet<SkillType> SkillTypes { get; set; }

    // Users table for user information.
    public virtual DbSet<User> Users { get; set; }
}
