using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyMannager.DTOs;

namespace CompanyMannager.Models
{
    /// <summary>
    /// Model for storing information about company in db
    /// </summary>
    public sealed class Company
    {
        /// <summary>
        /// Method that creates new company from dto
        /// </summary>
        /// <param name="companyDTO">company dto</param>
        /// <returns>company with initialized employees list</returns>
        public static Company GetNewFromDTO(CompanyDTO companyDTO)
        {
            Company company = new();
            company.UpdateByDTO(companyDTO);

            return company;
        }

        /// <summary>
        /// Method that updates current company with dto
        /// </summary>
        /// <param name="companyDTO">company dto</param>
        public void UpdateByDTO(CompanyDTO companyDTO)
        {
            Name = companyDTO.Name;
            EstablishmentYear = companyDTO.EstablishmentYear;
            Employees = new List<Employee>();

            if (companyDTO.Employees?.Count > 0)
            {
                foreach (var employeeDTO in companyDTO.Employees)
                {
                    Employees.Add(Employee.GetFromDTO(employeeDTO));
                }
            }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public List<Employee> Employees { get; set; }
    }
}