using System.Web.Routing;
using Ninject;
using System.Reflection;
using ODataSample.Repositories;
using ODataSample.Repositories.Models;
using ODataSample.Models;
using Ninject.Web.Common.WebHost;
using System.Web.Http;

namespace ODataSample
{
    public class WebApiApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected override Ninject.IKernel CreateKernel()
        {
            IKernel kernal = new StandardKernel();
            kernal.Load(Assembly.GetExecutingAssembly());
            kernal.Bind<IBaseRepository<Country>>().To<CountryRepository>();
            kernal.Bind<IBaseRepository<Product>>().To<ProductRepository>();
            kernal.Bind<IBaseRepository<Customer>>().To<CustomerRepository>();
            return kernal;
        }
    }
}
