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
            kernal.Bind<ICountryRepository>().To<CountryRepository>();
            kernal.Bind<IProductRepository> ().To<ProductRepository>();
            kernal.Bind<ICustomerRepository>().To<CustomerRepository>();
            return kernal;
        }
    }
}
