using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Student.Api.Data.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
    }
}
