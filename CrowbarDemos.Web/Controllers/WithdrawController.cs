using System.Web.Mvc;
using CrowbarDemos.Web.Models;

namespace CrowbarDemos.Web.Controllers
{
    public class WithdrawController : Controller
    {
        private readonly TellerService tellerService;

        public WithdrawController()
            : this(new TellerService(new SQLServerAccountRepository(), new InMemoryCashDispenser()))
        { }

        public WithdrawController(TellerService service)
        {
            tellerService = service;
        }

        public ActionResult Index()
        {
            return View("ATM", new Receipt());
        }

        [HttpPost]
        public ActionResult Withdraw(string accountNo, string pin, int amount)
        {
            // Authenticate
            tellerService.Authenticate(accountNo, pin);

            // And withdraw
            var receipt = tellerService.Withdraw(accountNo, amount);

            return View("ATM", receipt);
        }
    }
}
