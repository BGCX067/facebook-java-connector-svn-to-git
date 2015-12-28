using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using Rage.Addservice.Domain.Repositories;

using Rage.Addservice.Persistence.Repositories.Stubs;
using Rage.Addservice.Persistence.Repositories;

namespace Rage.Addservice.Host.PersistenceService
{
    public class Bootstrapper
    {
        private readonly IUnityContainer container;
        private readonly UnityServiceLocator locator;

        public Bootstrapper()
        {
            this.container = new UnityContainer();
            this.locator = new UnityServiceLocator(this.container);

            ServiceLocator.SetLocatorProvider(() => this.locator);
        }

        public void Run()
        {
            this.container.RegisterInstance<IUnityContainer>(this.container);

            this.RegisterRepositories();  
            //this.RegisterRepositoriesStubs();   // ---- if no database
        }

        private void RegisterRepositories()
        {
            this.container.RegisterInstance<IAdvertRepository>(new AdvertRepository());
            this.container.RegisterInstance<IWallRepository>(new WallRepository());
            this.container.RegisterInstance<IUserRepository>(new UserRepository());
        }

        private void RegisterRepositoriesStubs()
        {
            //temporary stubs here
            this.container.RegisterInstance<IAdvertRepository>(new AdvertRepositoryStub());
            this.container.RegisterInstance<IWallRepository>(new WallRepositoryStub());
            this.container.RegisterInstance<IUserRepository>(new UserRepositoryStub());
        }
    }
}