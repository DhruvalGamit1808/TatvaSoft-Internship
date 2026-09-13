using System.ComponentModel.DataAnnotations;

namespace StudentManagementAPI.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string StudentName { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

        [Range(0, 100)]
        public double Percentage { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}