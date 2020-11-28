using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CompanyMannager.DTOs;
using CompanyMannager.Models;
using System;
using System.Linq;

namespace CompanyMannager.Controllers
{
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompaniesRepository _companiesRepository;

        public CompanyController(
            ILogger<CompanyController> logger,
            ICompaniesRepository companiesRepository)
        {
            _logger = logger;
            _companiesRepository = companiesRepository;
        }


        [HttpPost("/company/create")]
        public async Task<IActionResult> Create(CompanyDTO companyDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            // add new company dto
            Company company = Company.GetNewFromDTO(companyDTO);
            await _companiesRepository.Add(company);

            // prepare response with created id
            var response = new { id = company.Id };
            return Ok(response);
        }

        [HttpPost("/company/search")]
        public async Task<IActionResult> Search(CompanySearchDTO searchDTO)
        {
            // get companies
            var companies = await _companiesRepository.Get(searchDTO);

            // prepare response with result companies
            var companiesDTOs = companies.Select(e => CompanyDTO.GetFromCompany(e));
            var response = new { Results = companiesDTOs };
            return Ok(response);
        }

        [HttpPut("/company/update/{id}")]
        public async Task<IActionResult> Update(long id, CompanyDTO companyDTO)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest();
                }

                // get company by id
                Company company = await _companiesRepository.GetById(id);
                // change data with with dto
                company.UpdateByDTO(companyDTO);
                // update model
                await _companiesRepository.Update(company);

                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound("None existing company");
            }
        }

        [HttpDelete("/company/delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                // delete company with id
                await _companiesRepository.Delete(id);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound("None existing company");
            }
        }
    }
}