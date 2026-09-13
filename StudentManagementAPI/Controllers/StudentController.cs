using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly StudentService studentService;

        public StudentController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(studentService.GetStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = studentService.GetStudent(id);

            if (student == null)
                return NotFound("Student not found.");

            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = studentService.AddStudent(student);

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = result.StudentId },
                result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = studentService.UpdateStudent(id, student);

            if (!result)
                return NotFound("Student not found.");

            return Ok("Student updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var result = studentService.DeleteStudent(id);

            if (!result)
                return NotFound("Student not found.");

            return Ok("Student deleted successfully.");
        }
    }
}