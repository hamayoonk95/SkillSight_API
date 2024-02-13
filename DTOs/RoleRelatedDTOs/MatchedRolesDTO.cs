using skillsight.API.DTOs;

public class MatchedRoleResponseDTO
{
    public required string Role { get; set; }
    public required List<CategoryTopSkillsDTO> TopSkills { get; set; }

    public JobRoleDetailsDTO? RoleDetails { get; set; }
}

public class CategoryTopSkillsDTO
{
    public required string Category { get; set; }
    public required List<SkillDTO> Skills { get; set; }
}

public class JobRoleDetailsDTO
{
    public string? Description { get; set; }
    public List<string>? Responsibilities { get; set; }
    public List<string>? Skills { get; set; }
    public string? FitsWell { get; set; }
}