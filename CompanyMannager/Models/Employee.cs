using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyMannager.DTOs;
using CompanyMannager.Enums;

namespace CompanyMannager.Models
{
    public sealed class Employee
    {
        public static Employee GetFromDTO(EmployeeDTO employeeDTO)
        {
            return new Employee()
            {
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                DateOfBirth = employeeDTO.DateOfBirth,
                JobTitle = employeeDTO.JobTitle
            };
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public JobTitle JobTitle { get; set; }

        [ForeignKey("Company")]
        public long CompanyId { get; set; }
        public Company Company { get; set; }
    }
}