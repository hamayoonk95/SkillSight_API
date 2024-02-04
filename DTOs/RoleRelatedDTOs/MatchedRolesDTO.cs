using skillsight.API.DTOs;

public class MatchedRoleResponseDTO
{
    public required string Role { get; set; }
    public required List<CategoryTopSkillsDTO> TopSkills { get; set; }
}

public class CategoryTopSkillsDTO
{
    public required string Category { get; set; }
    public required List<SkillDTO> Skills { get; set; }
}
