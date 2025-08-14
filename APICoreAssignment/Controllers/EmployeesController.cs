using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        // Returns all employees with their project details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees
                                 .Include(e => e.Project)
                                 .ToListAsync();
        }

        // GET: api/Employees/5
        // Returns one employee with their project details
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                                         .Include(e => e.Project)
                                         .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            // Ensure EmployeeCode is unique (excluding current employee)
            if (await _context.Employees.AnyAsync(e => e.EmployeeCode == employee.EmployeeCode && e.EmployeeId != id))
            {
                return Conflict(new { message = "EmployeeCode must be unique." });
            }

            // Ensure Email is unique (excluding current employee)
            if (await _context.Employees.AnyAsync(e => e.Email == employee.Email && e.EmployeeId != id))
            {
                return Conflict(new { message = "Email must be unique." });
            }

            // Ensure Project exists before updating
            if (!await _context.Projects.AnyAsync(p => p.ProjectId == employee.ProjectId))
            {
                return BadRequest(new { message = "Assigned Project does not exist." });
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            // Validate unique EmployeeCode
            if (await _context.Employees.AnyAsync(e => e.EmployeeCode == employee.EmployeeCode))
            {
                return Conflict(new { message = "EmployeeCode must be unique." });
            }

            // Validate unique Email
            if (await _context.Employees.AnyAsync(e => e.Email == employee.Email))
            {
                return Conflict(new { message = "Email must be unique." });
            }

            // Ensure Project exists before adding
            if (!await _context.Projects.AnyAsync(p => p.ProjectId == employee.ProjectId))
            {
                return BadRequest(new { message = "Assigned Project does not exist." });
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
