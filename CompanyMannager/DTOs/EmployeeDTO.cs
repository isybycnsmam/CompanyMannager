using System;
using System.ComponentModel.DataAnnotations;
using CompanyMannager.Enums;
using CompanyMannager.Models;

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
                JobTitle = employee.JobTitle
            };
        }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}