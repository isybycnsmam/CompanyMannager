using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CompanyMannager.Models;

namespace CompanyMannager.DTOs
{
    /// <summary>
    /// Dto for transferring data about company
    /// </summary>
    public class CompanyDTO
    {
        /// <summary>
        /// Method that gets company DTO from company model
        /// </summary>
        /// <param name="company">company model</param>
        /// <returns>CompanyDTO with initialized employees</returns>
        public static CompanyDTO GetFromCompany(Company company)
        {
            CompanyDTO companyDTO = new()
            {
                Name = company.Name,
                EstablishmentYear = company.EstablishmentYear,
                Employees = new List<EmployeeDTO>()
            };

            if (company.Employees?.Count > 0)
            {
                foreach (var employee in company.Employees)
                {
                    companyDTO.Employees.Add(EmployeeDTO.GetFromEmployee(employee));
                }
            }

            return companyDTO;
        }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int EstablishmentYear { get; set; }

        public List<EmployeeDTO> Employees { get; set; }
    }
}