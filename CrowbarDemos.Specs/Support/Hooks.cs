using Simple.Data;
using TechTalk.SpecFlow;

namespace CrowbarDemos.Specs.Support
{
    [Binding]
    public class Hooks
    {
        [BeforeTestRun]
        public static void BeforeAnyFeature()
        {
            // FOR DSL_INMEMORY ONLY
            // Database.UseMockAdapter(new InMemoryAdapter());            
        }

        [BeforeScenario]
        public void BeforeEveryScenario()
        {
            // FOR INMEMORY & HTML
            Database.Open().Accounts.DeleteAll();
        }
    }
}
