using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skillsight.API.Data;
using skillsight.API.DTOs;

// Namespace grouping API controllers
namespace skillsight.API.Controllers;

// RolesController handles role-related API requests
[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    // Database context for data access
    private readonly ApplicationDbContext _context;

    // Constructor to inject the database context
    public RolesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET endpoint to retrieve all roles
    // Route: api/roles/allRoles
    [HttpGet("allRoles")]
    public async Task<IActionResult> GetAllRoles()
    {
        // Fetch roles from database and map to RoleDTO
        var roles = await _context.JobRoles
            .Select(r => new RoleDTO
            {
                Id = r.Id,
                RoleTitle = r.RoleTitle
            })
            .ToListAsync();

        return Ok(roles);
    }

    // GET endpoint to retrieve skills by role ID
    // Route: api/roles/{RoleID}/skills
    [HttpGet("{RoleID}/skills")]
    public async Task<IActionResult> GetAllSkillsByRoleId(int roleId)
    {
        // Fetch skills associated with the given role ID
        var roleSkills = await _context.RoleSkills
            .Where(rs => rs.RoleId == roleId)
            .Include(rs => rs.Skill)
            .ThenInclude(s => s.Type)
            .Select(rs => new RoleSkillDTO
            {
                Skill = new SkillDTO
                {
                    Id = rs.Skill.Id,
                    SkillName = rs.Skill.SkillName,
                    Type = new SkillTypeDTO
                    {
                        Id = rs.Skill.Type.Id,
                        TypeName = rs.Skill.Type.TypeName
                    }
                },
                Frequency = rs.Frequency
            })
            .ToListAsync();

        // Return NotFound if no skills are associated with the role ID
        if (roleSkills == null || !roleSkills.Any())
        {
            return NotFound($"No skills found for role ID {roleId}.");
        }

        return Ok(roleSkills);
    }
}
