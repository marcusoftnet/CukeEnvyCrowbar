using Crowbar;
using CrowbarDemos.Web;
using CrowbarDemos.Web.Models;
using NUnit.Framework;

namespace CrowbarDemos.Specs.Support.Wrappers
{
    public class BankControllerWrapper
    {
        private readonly ICashDispenser _cashDispenser;

        public BankControllerWrapper(ICashDispenser cashDispenser)
        {
            _cashDispenser = cashDispenser;
        }

        public void Withdraw(string accountNo, string pinCode, int amount)
        {
            var app = MvcApplication.Create<AtmMvcApplication, AppProxy, AppProxyContext>("CrowbarDemos.Web");

            // AppGetter.CurrentApplication.Execute((client, context) => 
            app.Execute((client, context) =>
             {
                 // Let's bind the cash-dispenser to the 
                 // one that we've sent to the wrapper
                 context.Kernel.Rebind<ICashDispenser>().ToConstant(_cashDispenser);

                 // Post a HTTP form to our module                
                 var response = client.Post("/withdraw/withdraw", with =>
                         {
                             with.FormValue("accountNo", accountNo);
                             with.FormValue("pin", pinCode);
                             with.FormValue("amount", amount.ToString());
                         });

                 // Make sure that we've got an Accepted back
                 Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                 // We could even assert against the view
                 var actualAmount = response.AsCsQuery()[".dispensedAmount"].Text();
                 Assert.AreEqual(actualAmount, amount.ToString());
             });
        }
    }
}