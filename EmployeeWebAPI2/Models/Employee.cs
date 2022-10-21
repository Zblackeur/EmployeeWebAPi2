using System.ComponentModel.DataAnnotations;

namespace EmployeeWebAPI2.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100,MinimumLength =2)]
        public String FirstName { get; set; }
        [Required]

        public string LastName { get; set ; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int DepartementId { get; set; }

        public string PhotoPath { get; set; }

        public Gender gender { get; set; }
        public Departement? Departement { get; set; }

    }
   /* public enum Gender
    {
        Male, Female
    }*/
}
