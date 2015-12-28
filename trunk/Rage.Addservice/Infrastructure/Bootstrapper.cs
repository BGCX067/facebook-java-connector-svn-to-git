using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Rage.Addservice.Service;
using Rage.Addservice.Infrastructure;

namespace Rage.Addservice.Infrastructure
{
    public class Bootstrapper
    {
        private readonly IUnityContainer container;
        
        public IUnityContainer Container
        {
            get
            {
                return this.container;
            }
        }

        public Bootstrapper()
        {
            this.container = new UnityContainer();

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(this.container));
        }

        public void Run()
        {
            this.container.RegisterInstance<IUnityContainer>(this.container);
            this.container.RegisterInstance<IViewConductor>(new ViewConductor());
            this.container.RegisterInstance<IUserManager>(new UserManager());

            var persistenceService = new RemotePersistenceService();
            this.container.RegisterInstance<IPersistenceService>(persistenceService);
        }
    }
}
