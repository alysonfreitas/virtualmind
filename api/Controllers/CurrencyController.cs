using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using API.Repositories;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : Controller
    {
        private readonly ILogger _logger;

        public CurrencyController(ILogger<CurrencyController> logger)
        {
            _logger = logger;
        }

        // GET: api/currency/{iso}
        [HttpGet("{iso}")]
        public async Task<ActionResult<CurrencyModel>> GetCurrency(string iso)
        {
            _logger.LogInformation($"Invoking CurrencyController.GetCurrency: {iso}");

            CurrencyRepository currencyRepository = CurrencyRepository.GetCurrencyRepositoryByISO(iso);

            if (currencyRepository == null)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, new ErrorModel(_logger, "Sorry, the currency specified is not implemented yet."));
            }

            CurrencyModel currencyModel = await currencyRepository.GetCurrency();

            if (currencyModel == null)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, new ErrorModel(_logger, "Sorry, the currency specified is unavailable."));
            }

            return currencyModel;
        }
    }
}
