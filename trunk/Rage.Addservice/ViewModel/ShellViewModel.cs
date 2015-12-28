using System;
using Rage.Addservice.Service;
using Rage.Addservice.Infrastructure;
using System.Collections.ObjectModel;
using Rage.Addservice.ViewModel.Base;
using Rage.Addservice.Utill;
using Rage.Addservice.Domain.Model;
using System.Collections.Generic;

namespace Rage.Addservice.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly IPersistenceService persistenceService;
        private readonly IViewConductor viewConductor;

        private readonly Observable<AdvertsContainerViewModel> myAdverts;

        public ShellViewModel(IPersistenceService persistenceService, IViewConductor viewConductor) 
        {
            this.persistenceService = persistenceService;
            this.viewConductor = viewConductor;

            myAdverts = new Observable<AdvertsContainerViewModel>(new AdvertsContainerViewModel(persistenceService, viewConductor));

            IsLoggedIn();
        }

        void IsLoggedIn()
        {
            var user = new UserViewModel(persistenceService, OnUserLoggedIn, viewConductor);
            user.LogMeIn();
        }

        public void OnUserLoggedIn() 
        {
            this.persistenceService.GetAdverts(OnGetAdvertsCompleted);
        }

        private void OnGetAdvertsCompleted(List<Advert> adverts) 
        {
            this.myAdverts.Value.Initialize(adverts);
        }

        public Observable<AdvertsContainerViewModel> MyAdverts 
        {
            get { return this.myAdverts; }
        }
    }
}
