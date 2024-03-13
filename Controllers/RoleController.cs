using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skillsight.API.Data;
using skillsight.API.DTOs;

// Namespace grouping API controllers
namespace skillsight.API.Controllers;

/*********
    RolesController handles role-related API requests
    Attribute 'ApiController' is used to denote a controller with API-specific functionalities
**********/
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


    /******** 
        Retrieves all job roles from the database.
        Endpoint/Route: GET api/roles/allRoles
    ********/
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

    /******** 
    Retrieves skills for a specific role ID, filtered by category.
    Endpoint/Route: GET api/roles/{RoleID}/skills/{Category}
    ********/
    [HttpGet("{RoleID}/skills/{Category}")]
    public async Task<IActionResult> GetAllSkillsByRoleId(int roleId, string category, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        // Fetch skills associated with the given role ID where the role has job postings after the cutoff date
        var roleSkills = await _context.RoleSkills
            .Where(rs => rs.RoleId == roleId && rs.Skill.Type.TypeName.ToLower() == category.ToLower())
            .Include(rs => rs.Skill) // Include Skill details
            .Include(rs => rs.Role) // Include Role details
            .ThenInclude(role => role.JobPostings)
            .Where(rs => rs.Role.JobPostings.Any(jp => jp.DateScraped >= startDate && jp.DateScraped <= endDate))
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

        // Gets the top 20 skills from Role Skills
        var topSkills = roleSkills
            .GroupBy(rs => rs.Skill.Type.Id)
            .Select(g => g.OrderByDescending(rs => rs.Frequency).Take(20))
            .SelectMany(g => g.ToList());

        // Return the fetched skills as an HTTP 200 OK response.
        return roleSkills.Any() ? Ok(topSkills) : Ok(new List<RoleSkillDTO>());
    }

    /******** 
    Retrieves job posting information for a specific role ID.
    Endpoint/Route: GET api/roles/{RoleID}/jobInfo
    ********/
    [HttpGet("{RoleID}/jobInfo")]
    public async Task<IActionResult> GetJobInfoByRoleId(int roleId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {

        // Calculate the cutoff date as six months ago from the current date
        // int cutOffPeriod = -6;
        // DateTime cutOffDate = DateTime.Now.AddMonths(cutOffPeriod);

        // Fetch jobs count for a role by a particular roleId after a cutoff date
        var jobPostingsCount = await _context.JobPostings
            .Where(jp => jp.RoleId == roleId && jp.DateScraped >= startDate && jp.DateScraped <= endDate)
            .CountAsync();

        // Map cutOffDate and jobCount to JobInfoDTO
        var jobInfo = new JobInfoDTO
        {
            CutOffDate = startDate,
            JobCount = jobPostingsCount
        };

        // Return the fetched count
        return Ok(jobInfo);
    }

}
