using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skillsight.API.Data;
using skillsight.API.DTOs;

namespace skillsight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/roles/AllRoles
        [HttpGet("allRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _context.JobRoles
                .Select(r => new RoleDTO
                {
                    Id = r.Id,
                    RoleTitle = r.RoleTitle
                })
                .ToListAsync();

            return Ok(roles);
        }

        [HttpGet("{RoleID}/skills")]
        public async Task<IActionResult> GetAllSkillsByRoleId(int roleId)
        {
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

            if (roleSkills == null || !roleSkills.Any())
            {
                return NotFound($"No skills found for role ID {roleId}.");
            }

            return Ok(roleSkills);
        }
    }
}
