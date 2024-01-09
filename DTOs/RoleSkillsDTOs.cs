namespace skillsight.API.DTOs;

// DTO for Skill data
public class SkillDTO
{
    // Unique identifier for the Skill
    public int Id { get; set; }
    // Name of the Skill
    public required string SkillName { get; set; }
    // Associated SkillType for the Skill
    public required SkillTypeDTO Type { get; set; }
}

// DTO for SkillType data
public class SkillTypeDTO
{
    // Unique identifier for the SkillType
    public int Id { get; set; }
    // Name of the SkillType
    public required string TypeName { get; set; }
}

public class RoleSkillDTO
{
    // Skill associated with a role
    public required SkillDTO Skill { get; set; }
    // Frequency of the skill in the role
    public int Frequency { get; set; }
}

public class JobInfoDTO
{
    // CutOffDate for the jobs fetched
    public DateTime CutOffDate { get; set; }
    // count for the jobs for a role
    public int JobPostingsCount { get; set; }
}