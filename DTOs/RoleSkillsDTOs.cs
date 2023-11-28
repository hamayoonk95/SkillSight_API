public class SkillDTO
{
    public int Id { get; set; }
    public required string SkillName { get; set; }
    public required SkillTypeDTO Type { get; set; }
}

public class SkillTypeDTO
{
    public int Id { get; set; }
    public required string TypeName { get; set; }
}

public class RoleSkillDTO
{
    public required SkillDTO Skill { get; set; }
    public int? Frequency { get; set; }
}
