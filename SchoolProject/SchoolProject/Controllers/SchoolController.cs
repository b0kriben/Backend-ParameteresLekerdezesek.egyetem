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
            // 1. query format
            var result1 = from s in _context.Students
                         from d in _context.Departments
                         where s.DepartmentId == d.Id && d.Name == departmentName
                         select s.Name;

            // 2. method format
            var result = _context.Students
                        .Join(_context.Departments, s => s.DepartmentId, d => d.Id, (s, d) => new { Student = s, Department = d })
                        .Where(sd => sd.Department.Name == departmentName)
                        .Select(sd => sd.Student.Name);
            return Ok(result.ToListAsync());
        }

        //4. Endpoint: `GET /departments/{department_id}/students/count`  
        //Feladat: Készíts egy végpontot, amely visszaadja egy adott tanszékhez tartozó diákok számát.
        [HttpGet("/departments /{department_id}/students/count")]
        public async Task<IActionResult> GetNumberOfStudentByDepartment(int department_id)
        {
            return Ok(await _context.Students.Where(s => s.DepartmentId == department_id).CountAsync());
        }

        // Többtánlás változat
        [HttpGet("/departments/students/count")]
        public async Task<IActionResult> GetNumberOfStudentByDepartment([FromQuery] string departmentName)
        {
            // 1. query format
            var result1 = from s in _context.Students
                         from d in _context.Departments
                         where s.DepartmentId == d.Id && d.Name == departmentName
                         select s.Name;

            // 2. method format
            var result = _context.Students
                        .Join(_context.Departments, s => s.DepartmentId, d => d.Id, (s, d) => new { Student = s, Department = d })
                        .Where(sd => sd.Department.Name == departmentName)
                        .Select(sd => sd.Student.Name);
            return Ok(result.ToListAsync());
        }

        //Projekciós feladatok
        //1. Endpoint: `GET /students`  
        //Feladat: Készíts egy végpontot, amely visszaadja az összes diák nevét.
        [HttpGet("/studentsName")]
        public async Task<IActionResult> GetStudentsName()
        {
            return Ok(await _context.Students.Select(s => s.Name).ToListAsync());
        }

        //3. Endpoint: `GET /students`  
        //Feladat: Készíts egy végpontot, amely visszaadja az összes diák nevét és email címét.
        [HttpGet("/studentsNameAndEmail")]
        public async Task<IActionResult> GetStudentsNameAndEmail()
        {
            return Ok(await _context.Students.Select(s => new {s.Name, s.Email}).ToListAsync());
        }

        [HttpGet("/studentsNameAndEmail2")]
        public async Task<IActionResult> GetStudentsNameAndEmail2()
        {
            return Ok(await _context.Students.Select(s => new NameAndEmail{Name = s.Name, Email = s.Email}).ToListAsync());
        }
    }
}
