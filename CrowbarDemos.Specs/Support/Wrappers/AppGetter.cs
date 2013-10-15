using Crowbar;
using CrowbarDemos.Web;
using TechTalk.SpecFlow;

namespace CrowbarDemos.Specs.Support.Wrappers
{
    public static class AppGetter
    {
        private const string CURRENTAPP = "CurrentApplication";

        public static MvcApplication<AtmMvcApplication, AppProxyContext> CurrentApplication
        {
            get
            {
                if (!FeatureContext.Current.ContainsKey(CURRENTAPP))
                {
                    var app = MvcApplication.Create<AtmMvcApplication, AppProxy, AppProxyContext>("CrowbarDemos.Web");
                    FeatureContext.Current.Add(CURRENTAPP, app);
                }

                return FeatureContext.Current.Get<MvcApplication<AtmMvcApplication, AppProxyContext>>(CURRENTAPP);
            }
        }
    }
}
