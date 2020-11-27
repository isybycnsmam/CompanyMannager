using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyMannager.DTOs;

namespace CompanyMannager.Models
{
    /// <summary>
    /// Model for storing information about employee
    /// </summary>
    public sealed class Employee
    {
        /// <summary>
        /// Method that gets employee model from emplyee dto
        /// </summary>
        /// <param name="employeeDTO">employee dto</param>
        /// <returns>employee model</returns>
        public static Employee GetFromDTO(EmployeeDTO employeeDTO)
        {
            return new Employee()
            {
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                DateOfBirth = employeeDTO.DateOfBirth.Value,
                JobTitle = employeeDTO.JobTitle
            };
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string JobTitle { get; set; }

        [ForeignKey("Company")]
        public long CompanyId { get; set; }
        public Company Company { get; set; }
    }
}