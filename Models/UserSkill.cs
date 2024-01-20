// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using Microsoft.EntityFrameworkCore;

// namespace skillsight.API.Models;

// // Specifies the database table name for the model
// [Table("user_skills")]
// // Indexes for efficient querying by SkillId and UserId
// // [Index("SkillId", Name = "skill_id")]
// // [Index("UserId", Name = "user_id")]
// public partial class UserSkill
// {
//     // Primary key of the table
//     [Key]
//     [Column("id")]
//     public int Id { get; set; }

//     // Foreign key relationship to the User model
//     [Column("user_id")]
//     public int UserId { get; set; }

//     // Foreign key relationship to the Skill model
//     [Column("skill_id")]
//     public int SkillId { get; set; }

//     // // Navigation property to the Skill model
//     // [ForeignKey("SkillId")]
//     // [InverseProperty("UserSkills")]
//     // public virtual required Skill Skill { get; set; }

//     // // Navigation property to the User model
//     // [ForeignKey("UserId")]
//     // [InverseProperty("UserSkills")]
//     // public virtual required User User { get; set; }
// }
