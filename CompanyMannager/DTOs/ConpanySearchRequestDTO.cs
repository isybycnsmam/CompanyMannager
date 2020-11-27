using System;
using System.Collections.Generic;

namespace CompanyMannager.DTOs
{
    /// <summary>
    /// Dto for transferring search conditions
    /// </summary>
    public sealed class CompanySearchDTO
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public List<string> EmployeeJobTitles { get; set; }
    }
}