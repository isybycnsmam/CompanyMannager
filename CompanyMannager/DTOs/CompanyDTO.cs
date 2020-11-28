using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CompanyMannager.Models;

namespace CompanyMannager.DTOs
{
    public class CompanyDTO
    {
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
        public int EstablishmentYear { get; set; }
        public List<EmployeeDTO> Employees { get; set; }
    }
}