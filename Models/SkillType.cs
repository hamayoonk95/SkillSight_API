using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skillsight.API.Models;

// Specifies the database table name for the model
[Table("skill_types")]
public partial class SkillType
{
    // Primary key of the table
    [Key]
    [Column("id")]
    public int Id { get; set; }

    // Type name field
    [Column("type_name")]
    [StringLength(50)]
    public required string TypeName { get; set; }

    // Collection navigation property for Skills of this SkillType
    [InverseProperty("Type")]
    public virtual ICollection<Skill> Skills { get; } = new List<Skill>();
}
