using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using skillsight.API.Data;
using skillsight.API.DTOs;
using skillsight.API.Models;

// Namespace grouping API controllers
namespace skillsight.API.Controllers;

/*********
    RoleMatchingController handles role matching API requests.
    Provides functionality to match user answers to a specific role.
**********/
[ApiController]
[Route("api/[controller]")]
public class RoleMatchingController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IRoleMatchingService _roleMatchingService;
    public RoleMatchingController(ApplicationDbContext context, IRoleMatchingService roleMatchingService)
    {
        _context = context;
        _roleMatchingService = roleMatchingService;
    }

    /******** 
        Matches user answers to a role and retrieves related skill information.
        Endpoint/Route: POST api/roleMatching/match
    ********/
    [HttpPost("match")]
    public async Task<ActionResult<MatchedRoleResponseDTO>> MatchRoles([FromBody] Dictionary<string, string> answers)
    {
        // Gets the role for the answers from the user (Prcoessed by Role Matching Service)
        var matchedRoleTitle = await _roleMatchingService.MatchUserToRoleAsync(answers);

        // Get the title of the job role from database
        var jobRole = await GetJobRoleByTitleAsync(matchedRoleTitle);

        // return error if job role not found
        if (jobRole == null)
        {
            return NotFound("Role not found.");
        }

        // Retrieves Top 3 skills per category per job role from the Database
        var topSkillsByCategory = await GetTopSkillsByCategoryAsync(jobRole.Id);

        // Deserialize the JSON details property into JobRoleDetailsDTO
        JobRoleDetailsDTO jobRoleDetails = JsonSerializer.Deserialize<JobRoleDetailsDTO>(jobRole.Details) ?? new JobRoleDetailsDTO();

        // Create the Response DTO
        var response = new MatchedRoleResponseDTO
        {
            Role = jobRole.RoleTitle,
            RoleDetails = jobRoleDetails,
            TopSkills = topSkillsByCategory
        };
        // Return response
        return Ok(response);
    }

    // Retrieves job role by title
    private async Task<JobRole> GetJobRoleByTitleAsync(string roleTitle)
    {
        // Fetches a job role based on its title
        return await _context.JobRoles.FirstOrDefaultAsync(r => r.RoleTitle == roleTitle) ?? throw new InvalidOperationException("Job Role not found");
    }

    // Retrieves top skills by category for a given role ID
    private async Task<List<CategoryTopSkillsDTO>> GetTopSkillsByCategoryAsync(int roleId)
    {
        // Fetches skills by role ID and groups them by category
        var roleSkills = await _context.RoleSkills
            .Where(rs => rs.RoleId == roleId)
            .Include(rs => rs.Skill)
            .ThenInclude(s => s.Type)
            .ToListAsync();

        // Group skills by category
        var groupedSkills = roleSkills.GroupBy(rs => rs.Skill.Type);

        // Create a list to store the top skills per category
        var topSkillsByCategory = new List<CategoryTopSkillsDTO>();
        System.Console.WriteLine(groupedSkills);
        // Iterate over each group (category) and select the top 3 skills
        foreach (var group in groupedSkills)
        {
            var topSkills = group.OrderByDescending(rs => rs.Frequency)
                .Take(3)
                .Select(rs => new SkillDTO
                {
                    Id = rs.Skill.Id,
                    SkillName = rs.Skill.SkillName,
                    Type = new SkillTypeDTO
                    {
                        Id = rs.Skill.Type.Id,
                        TypeName = rs.Skill.Type.TypeName
                    }
                }).ToList();

            topSkillsByCategory.Add(new CategoryTopSkillsDTO
            {
                Category = group.Key.TypeName,
                Skills = topSkills
            });
        }

        return topSkillsByCategory;
    }
}