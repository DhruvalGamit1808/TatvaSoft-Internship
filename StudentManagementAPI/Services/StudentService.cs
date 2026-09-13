using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Services
{
    public class StudentService
    {
        private readonly AppDbContext context;

        public StudentService(AppDbContext context)
        {
            this.context = context;
        }

        public List<Student> GetStudents()
        {
            return context.Students
                .AsNoTracking()
                .ToList();
        }

        public Student GetStudent(int id)
        {
            return context.Students.Find(id);
        }

        public Student AddStudent(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
            return student;
        }

        public bool UpdateStudent(int id, Student student)
        {
            var existingStudent = context.Students.Find(id);

            if (existingStudent == null)
                return false;

            existingStudent.StudentName = student.StudentName;
            existingStudent.Age = student.Age;
            existingStudent.Percentage = student.Percentage;
            existingStudent.EmailAddress = student.EmailAddress;

            context.SaveChanges();

            return true;
        }

        public bool DeleteStudent(int id)
        {
            var student = context.Students.Find(id);

            if (student == null)
                return false;

            context.Students.Remove(student);
            context.SaveChanges();

            return true;
        }
    }
}