using System;
using System.ComponentModel.DataAnnotations;
using CompanyMannager.Enums;
using CompanyMannager.Models;
using CompanyMannager.Validation;

namespace CompanyMannager.DTOs
{
    /// <summary>
    /// Dto for transferring data about employee
    /// </summary>
    public class EmployeeDTO
    {
        /// <summary>
        /// Method that gets employee dto from employee model
        /// </summary>
        /// <param name="employee">employee model</param>
        /// <returns>employee dto</returns>
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

        [EnumAsString(TargetEnum = typeof(JobTitle))]
        public string JobTitle { get; set; }
    }
}