using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyMannager.DTOs;

namespace CompanyMannager.Models
{
    public sealed class Company
    {
        public static Company GetNewFromDTO(CompanyDTO companyDTO)
        {
            Company company = new();
            company.UpdateByDTO(companyDTO);

            return company;
        }

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