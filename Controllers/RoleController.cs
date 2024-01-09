using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skillsight.API.Data;
using skillsight.API.DTOs;

// Namespace grouping API controllers
namespace skillsight.API.Controllers;

// RolesController handles role-related API requests
// Attribute 'ApiController' is used to denote a controller with API-specific functionalities
[ApiController]
// Attribute 'Route' defines the route template for this controller
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

        // Return the fetched roles as an HTTP 200 OK response
        return Ok(roles);
    }

    // GET endpoint to retrieve skills by role ID
    // Route: api/roles/{RoleID}/skills
    [HttpGet("{RoleID}/skills")]
    public async Task<IActionResult> GetAllSkillsByRoleId(int roleId)
    {
        // Calculate the cutoff date as six months ago from the current date
        int cutOffPeriod = -6;
        DateTime cutOffDate = DateTime.Now.AddMonths(cutOffPeriod);

        // Fetch skills associated with the given role ID where the role has job postings after the cutoff date
        var roleSkills = await _context.RoleSkills
            .Where(rs => rs.RoleId == roleId)
            .Include(rs => rs.Skill)
            .Include(rs => rs.Role)
            .ThenInclude(role => role.JobPostings.Where(jp => jp.DateScraped >= cutOffDate))
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
                Frequency = rs.Frequency,
            })
            .ToListAsync();

        // Return NotFound if no skills are associated with the role ID
        if (roleSkills == null || !roleSkills.Any())
        {
            return NotFound($"No skills found for role ID {roleId}.");
        }
        
        // Return the fetched skills as an HTTP 200 OK response.
        return Ok(roleSkills);
    }


    // GET endpoint to retrieve job information
    // Route: api/roles/{RoleID}/jobInfo
    [HttpGet("{RoleID}/jobInfo")]
    public async Task<IActionResult> GetJobInfoByRoleId(int roleId)
    {
        
        // Calculate the cutoff date as six months ago from the current date
        int cutOffPeriod = -6;
        DateTime cutOffDate = DateTime.Now.AddMonths(cutOffPeriod);
        
        // Fetch jobs count for a role by a particular roleId after a cutoff date
        var jobPostingsCount = await _context.JobPostings
            .Where(jp => jp.RoleId == roleId && jp.DateScraped >= cutOffDate)
            .CountAsync();

        // Map cutOffDate and jobCount to JobInfoDTO
        var jobInfo = new JobInfoDTO
        {
            CutOffDate = cutOffDate,
            JobPostingsCount = jobPostingsCount
        };

        // Return the fetched count
        return Ok(jobInfo);
    }

}
