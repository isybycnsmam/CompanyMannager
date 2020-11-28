using System;
using System.ComponentModel.DataAnnotations;
using CompanyMannager.Enums;
using CompanyMannager.Models;
using CompanyMannager.Validation;

namespace CompanyMannager.DTOs
{
    public class EmployeeDTO
    {
        public static EmployeeDTO GetFromEmployee(Employee employee)
        {
            return new EmployeeDTO()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                JobTitle = employee.JobTitle.ToString()
            };
        }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [EnumAsString(TargetEnum = typeof(JobTitle))]
        public string JobTitle { get; set; }
    }
}