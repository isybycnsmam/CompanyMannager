using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyMannager.Controllers
{
    [ApiController]
	public class CompanyController : ControllerBase
	{
		private readonly ILogger<CompanyController> _logger;

		public CompanyController(ILogger<CompanyController> logger)
		{
			_logger = logger;
		}
	}
}