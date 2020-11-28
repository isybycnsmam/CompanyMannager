using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyMannager.DTOs;
using CompanyMannager.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyMannager
{
    /// <summary>
    /// Repository for mannaging companies inside db
    /// </summary>
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly CompanyContext _dbContext;

        public CompaniesRepository(CompanyContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Add(Company companyToUpdate)
        {
            await _dbContext.Companies.AddAsync(companyToUpdate);
            await SaveAsync();
        }

        public async Task Delete(long id)
        {
            // get company with employees
            Company company = await _dbContext.Companies
                .Include(e => e.Employees)
                .FirstAsync(e => e.Id == id);
            // remove it and owned employees
            _dbContext.Companies.Remove(company);
            // save
            await SaveAsync();
        }

        public async Task<List<Company>> Get(CompanySearchDTO searchDTO)
        {
            // get all companies query with employees attached
            var companies = _dbContext.Companies
                .Include(e => e.Employees)
                .AsQueryable();

            // apply keyword filter(for company name or employee first/last name contains)
            if (searchDTO.Keyword is not null)
            {
                companies = companies.Where(c =>
                    c.Name.Contains(searchDTO.Keyword) ||
                    c.Employees.Any(e =>
                        e.FirstName.Contains(searchDTO.Keyword) ||
                        e.LastName.Contains(searchDTO.Keyword)));
            }

            // apply employy birth date filter
            if (searchDTO.EmployeeDateOfBirthFrom is not null || searchDTO.EmployeeDateOfBirthTo is not null)
            {
                companies = companies.Where(e =>
                    e.Employees.Any(e =>
                        (searchDTO.EmployeeDateOfBirthFrom != null ? e.DateOfBirth >= searchDTO.EmployeeDateOfBirthFrom : true) &&
                        (searchDTO.EmployeeDateOfBirthTo != null ? e.DateOfBirth <= searchDTO.EmployeeDateOfBirthTo : true)));
            }

            // apply job titles filter
            if (searchDTO.EmployeeJobTitles?.Count > 0)
            {
                companies = companies.Where(e =>
                    e.Employees.Any(e => searchDTO.EmployeeJobTitles.Contains(e.JobTitle.ToString())));
            }

            return await companies.ToListAsync();
        }

        public async Task<Company> GetById(long id)
        {
            return await _dbContext.Companies
                .Include(e => e.Employees)
                .FirstAsync(e => e.Id == id);
        }

        public async Task Update(Company companyToUpdate)
        {
            _dbContext.Companies.Update(companyToUpdate);
            await SaveAsync();
        }

        private async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Interface for declaring repository that mannages companies
    /// </summary>
    public interface ICompaniesRepository
    {
        Task Update(Company companyToUpdate);
        Task Add(Company companyToUpdate);
        Task Delete(long id);
        Task<Company> GetById(long id);
        Task<List<Company>> Get(CompanySearchDTO searchDTO);
    }
}