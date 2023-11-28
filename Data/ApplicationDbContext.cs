using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using skillsight.API.Models;

namespace skillsight.API.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<JobPosting> JobPostings { get; set; }

    public virtual DbSet<JobRole> JobRoles { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<RoleSkill> RoleSkills { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SkillType> SkillTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSkill> UserSkills { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseMySql("server=localhost;port=3306;user=hamayoon;password=hamayoon;database=skill_data", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder
    //         .UseCollation("utf8mb4_0900_ai_ci")
    //         .HasCharSet("utf8mb4");

    //     modelBuilder.Entity<Efmigrationshistory>(entity =>
    //     {
    //         entity.HasKey(e => e.MigrationId).HasName("PRIMARY");
    //     });

    //     modelBuilder.Entity<JobPosting>(entity =>
    //     {
    //         entity.HasKey(e => e.Id).HasName("PRIMARY");

    //         entity.HasOne(d => d.Role).WithMany(p => p.JobPostings).HasConstraintName("job_postings_ibfk_1");
    //     });

    //     modelBuilder.Entity<JobRole>(entity =>
    //     {
    //         entity.HasKey(e => e.Id).HasName("PRIMARY");
    //     });

    //     modelBuilder.Entity<Project>(entity =>
    //     {
    //         entity.HasKey(e => e.Id).HasName("PRIMARY");

    //         entity.HasOne(d => d.Role).WithMany(p => p.Projects).HasConstraintName("projects_ibfk_2");

    //         entity.HasOne(d => d.User).WithMany(p => p.Projects).HasConstraintName("projects_ibfk_1");
    //     });

    //     modelBuilder.Entity<RoleSkill>(entity =>
    //     {
    //         entity.HasKey(e => e.Id).HasName("PRIMARY");

    //         entity.HasOne(d => d.Role).WithMany(p => p.RoleSkills).HasConstraintName("role_skills_ibfk_1");

    //         entity.HasOne(d => d.Skill).WithMany(p => p.RoleSkills).HasConstraintName("role_skills_ibfk_2");
    //     });

    //     modelBuilder.Entity<Skill>(entity =>
    //     {
    //         entity.HasKey(e => e.Id).HasName("PRIMARY");

    //         entity.HasOne(d => d.Type).WithMany(p => p.Skills).HasConstraintName("skills_ibfk_1");
    //     });

    //     modelBuilder.Entity<SkillType>(entity =>
    //     {
    //         entity.HasKey(e => e.Id).HasName("PRIMARY");
    //     });

    //     modelBuilder.Entity<User>(entity =>
    //     {
    //         entity.HasKey(e => e.Id).HasName("PRIMARY");
    //     });

    //     modelBuilder.Entity<UserSkill>(entity =>
    //     {
    //         entity.HasKey(e => e.Id).HasName("PRIMARY");

    //         entity.HasOne(d => d.Skill).WithMany(p => p.UserSkills).HasConstraintName("user_skills_ibfk_2");

    //         entity.HasOne(d => d.User).WithMany(p => p.UserSkills).HasConstraintName("user_skills_ibfk_1");
    //     });

    //     OnModelCreatingPartial(modelBuilder);
    // }

    // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
