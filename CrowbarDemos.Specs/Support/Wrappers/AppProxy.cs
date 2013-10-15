using System;
using Crowbar;
using CrowbarDemos.Web;
using Ninject;

namespace CrowbarDemos.Specs.Support.Wrappers
{
    public class AppProxy : MvcApplicationProxyBase<AtmMvcApplication, AppProxyContext>
    {
        protected override AppProxyContext CreateContext(AtmMvcApplication application, string testBaseDirectory)
        {
            return new AppProxyContext(application.Kernel);
        }
    }

    public class AppProxyContext : IDisposable
    {
        public AppProxyContext() { }
        public AppProxyContext(IKernel kernel)
        {
            Kernel = kernel;
        }

        public IKernel Kernel { get; private set; }

        public void Dispose() { }
    }
}