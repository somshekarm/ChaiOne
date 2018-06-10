using ManifestDbContext;
using ManifestRepository;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace ManifestResource
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            container.RegisterType<IManifestUnitOfWork, ManifestUnitOfWork>();
            container.RegisterType<ManifestRepository.IManifestRepository, ManifestRepository.ManifestRepository>();
            container.RegisterType<IAppServerRepository, AppServerRepository>();
        }
    }
}