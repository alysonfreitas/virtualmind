using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using API.Models;
using API.Entities;
using API.Repositories;
using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly EntityContext _context;
        private readonly ILogger _logger;

        public TransactionController(EntityContext context, ILogger<TransactionController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // POST: api/currency
        [HttpPost()]
        public async Task<ActionResult<TransactionEntity>> NewTransaction(TransactionModel transaction)
        {
            _logger.LogInformation($"Invoking TransactionController.NewTransaction: {JsonConvert.SerializeObject(transaction)}");

            if (transaction.CurrencyCode == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(_logger, "CurrencyCode cannot be null."));
            }

            if (transaction.Amount <= 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(_logger, "Amount must to be more than 0."));
            }

            CurrencyRepository currencyRepository = CurrencyRepository.GetCurrencyRepositoryByISO(transaction.CurrencyCode);

            if (currencyRepository == null)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, new ErrorModel(_logger, "Sorry, the currency specified is not implemented yet."));
            }

            // get selected currency
            CurrencyModel currency = await currencyRepository.GetCurrency();

            // find user
            UserEntity user = this._context.Users.AsNoTracking().Include(u => u.Transactions)
            .Where(u => u.Id == transaction.UserId).FirstOrDefault();

            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(_logger, "User not found."));
            }

            decimal currentAmountPurchased = transaction.Amount / currency.PurchasePrice;
            DateTime lastMonth = DateTime.Now.AddMonths(-1);
            decimal userTotalPurchasesFromLastMonth = user.Transactions
                .Where(t => t.CreatedAt >= lastMonth)
                .Where(t => t.CurrencyCode == currency.ISO)
                .Sum(t => t.AmountPurchased);

            if ((userTotalPurchasesFromLastMonth + currentAmountPurchased) >= currencyRepository.GetPurchaseLimit())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(_logger, $"You cannot purchase more than {currencyRepository.GetPurchaseLimit()} {currencyRepository.GetISO()} per month."));
            }

            TransactionEntity trans = new TransactionEntity();
            trans.Amount = transaction.Amount;
            trans.AmountPurchased = currentAmountPurchased;
            trans.CurrencyCode = currency.ISO;
            trans.CreatedAt = DateTime.Now;
            trans.UserId = transaction.UserId;

            this._context.Transactions.Add(trans);
            this._context.SaveChanges();

            return trans;
        }
    }
}
