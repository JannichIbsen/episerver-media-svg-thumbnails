using EPi.Media.Svg.Thumbnails.Manager;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.ServiceLocation;
using EPiServer.Framework.Initialization;
using System.Web.Mvc;
using EPi.Media.Svg.Thumbnails.DI;

namespace EPi.Media.Svg.Thumbnails.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DependencyResolverInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Container.Configure((container) =>
            {
                container.For<ThumbnailManager>().Use<CustomThumbnailManager>();
            });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.Container));
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}
