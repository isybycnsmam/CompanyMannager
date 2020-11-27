using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyMannager.DTOs;
using CompanyMannager.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyMannager
{
    /// <summary>
    /// Interface for mannaging companies with its emplyees
    /// </summary>
    public interface ICompaniesRepository
    {
        /// <summary>
        /// Method fot updating company information
        /// </summary>
        /// <param name="companyToUpdate">company to update</param>
        Task Update(Company companyToUpdate);

        /// <summary>
        /// Method that adds new company
        /// </summary>
        /// <param name="companyToUpdate">conmpany to insert</param>
        Task Add(Company companyToUpdate);

        /// <summary>
        /// Method that deletes company with its employees
        /// </summary>
        /// <param name="id">company id</param>
        Task Delete(long id);

        /// <summary>
        /// Method that gets company with employees by id
        /// </summary>
        /// <param name="id">company id</param>
        /// <returns>company model</returns>
        Task<Company> GetById(long id);

        /// <summary>
        /// Method that applies filters and gets companies with employees
        /// </summary>
        /// <param name="searchDTO">filters object</param>
        /// <returns>list of matching companies</returns>
        Task<List<Company>> Get(CompanySearchDTO searchDTO);
    }

    /// <summary>
    /// Repository for mannaging companies inside db
    /// </summary>
    /// <inheritdoc/>
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly CompanyContext _dbContext;

        public CompaniesRepository(CompanyContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method that adds and saves company to db
        /// </summary>
        public async Task Add(Company companyToUpdate)
        {
            await _dbContext.Companies.AddAsync(companyToUpdate);
            await SaveAsync();
        }

        /// <summary>
        /// Method that deletes and saves company to db
        /// </summary>
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

        /// <summary>
        /// Method that gets async matching companies from db
        /// </summary>
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

        /// <summary>
        /// Method that gets company with all its employees from db
        /// </summary>
        public async Task<Company> GetById(long id)
        {
            return await _dbContext.Companies
                .Include(e => e.Employees)
                .FirstAsync(e => e.Id == id);
        }

        /// <summary>
        /// Method that updates and saves information about company and employees
        /// </summary>
        public async Task Update(Company companyToUpdate)
        {
            _dbContext.Companies.Update(companyToUpdate);
            await SaveAsync();
        }

        /// <summary>
        /// Method that saves changes into db
        /// </summary>
        private async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}