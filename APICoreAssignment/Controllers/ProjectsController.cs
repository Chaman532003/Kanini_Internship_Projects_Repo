using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        // Returns all projects with employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects
                                 .Include(p => p.Employees)
                                 .ToListAsync();
        }

        // GET: api/Projects/5
        // Returns single project with employees
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects
                                        .Include(p => p.Employees)
                                        .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            // Check for unique ProjectCode
            if (await _context.Projects.AnyAsync(p => p.ProjectCode == project.ProjectCode && p.ProjectId != id))
            {
                return Conflict(new { message = "ProjectCode must be unique." });
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            // Validate uniqueness
            if (await _context.Projects.AnyAsync(p => p.ProjectCode == project.ProjectCode))
            {
                return Conflict(new { message = "ProjectCode must be unique." });
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects
                                        .Include(p => p.Employees)
                                        .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }

            // Remove employees first (to avoid FK constraint errors)
            _context.Employees.RemoveRange(project.Employees);

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}
