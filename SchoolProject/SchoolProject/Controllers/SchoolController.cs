using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolContext _context = new SchoolContext();

        [HttpGet("students")]
        public async Task<IActionResult> GetAllStudentAsync()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetAllDepartmentAsync()
        {
            return Ok(await _context.Departments.ToListAsync());
        }

        [HttpGet("studentsOfDepartment")]
        public async Task<IActionResult> GetStudentsOfDepartmentAsync([FromQuery] string departmentName)
        {
            var result = (from s in _context.Students
                         from d in _context.Departments
                         where s.DepartmentId == d.Id && d.Name == departmentName
                         select s.Name).ToListAsync();
            return Ok(await result);
        }
    }
}
