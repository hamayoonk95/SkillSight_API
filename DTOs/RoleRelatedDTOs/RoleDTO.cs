namespace skillsight.API.DTOs;

// DTO for Role data
public class RoleDTO
{
    // Unique identifier for the Role
    public int Id { get; set; }
    // Title of the Role, required field
    public required string RoleTitle { get; set; }
}
