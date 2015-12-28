using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Rage.Addservice.Domain.Model;
using Rage.Addservice.RemoteService;
using Rage.Addservice.Services;
using System.Collections.Generic;
using System.ComponentModel;

namespace Rage.Addservice.Service
{
    public class RemotePersistenceService : IPersistenceService
    {

        private readonly PersistenceServiceClient serviceClient;

        public RemotePersistenceService() 
        {
            var address = new Uri(Application.Current.Host.Source, "../PersistenceService/PersistenceService.svc");
            this.serviceClient = new PersistenceServiceClient("BasicHttpBinding_IPersistenceService", address.AbsoluteUri);


            this.serviceClient.CreateAdvertCompleted += new EventHandler<CreateAdvertCompletedEventArgs>(CreateAdvertCompleted);
            this.serviceClient.GetAdvertCompleted += new EventHandler<GetAdvertCompletedEventArgs>(GetAdvertCompleted);
            this.serviceClient.GetAdvertsCompleted += new EventHandler<GetAdvertsCompletedEventArgs>(GetAdvertsCompleted);
            this.serviceClient.GetAdvertStatusCompleted += new EventHandler<GetAdvertStatusCompletedEventArgs>(GetAdvertStatusCompleted);

            this.serviceClient.GetUsersCompleted += new EventHandler<GetUsersCompletedEventArgs>(GetUsersCompleted);
            this.serviceClient.IsLoggedInCompleted += new EventHandler<IsLoggedInCompletedEventArgs>(IsLoggedInCompleted);
            this.serviceClient.LoginCompleted += new EventHandler<LoginCompletedEventArgs>(LoginCompleted);
            this.serviceClient.UpdateUserCompleted += new EventHandler<AsyncCompletedEventArgs>(UpdateUserCompleted);

            this.serviceClient.GetWallCompleted += new EventHandler<GetWallCompletedEventArgs>(GetWallCompleted);
            this.serviceClient.GetWallsCompleted += new EventHandler<GetWallsCompletedEventArgs>(GetWallsCompleted);
        }

        private void GetAdvertCompleted(object sender, GetAdvertCompletedEventArgs e)
        {
            var state = (ServiceCallState<Advert>)e.UserState;
            state.Complete(e, () => e.Result);
        }

        private void GetAdvertsCompleted(object sender, GetAdvertsCompletedEventArgs e) 
        {
            var state = (ServiceCallState<List<Advert>>)e.UserState;
            state.Complete(e, () => e.Result);
        }

        private void CreateAdvertCompleted(object sender, CreateAdvertCompletedEventArgs e)
        {
            var state = (ServiceCallState<int?>)e.UserState;
            state.Complete(e, () => e.Result);
        }

        private void GetAdvertStatusCompleted(object sender, GetAdvertStatusCompletedEventArgs e) 
        {
            var state = (ServiceCallState<AdvertStatus>)e.UserState;
            state.Complete(e, () => e.Result);
        }

        private void GetUsersCompleted(object sender, GetUsersCompletedEventArgs e) 
        {
            var state = (ServiceCallState<List<User>>)e.UserState;
            state.Complete(e, () => e.Result);
        }

        private void IsLoggedInCompleted(object sender, IsLoggedInCompletedEventArgs e) 
        {
            var state = (ServiceCallState<User>)e.UserState;
            state.Complete(e, () => e.Result);
        }

        private void LoginCompleted(object sender, LoginCompletedEventArgs e) 
        {
            var state = (ServiceCallState<User>)e.UserState;
            state.Complete(e, () => e.Result);
        }

        private void UpdateUserCompleted(object sender, AsyncCompletedEventArgs e) 
        {
            var state = (ServiceCallState)e.UserState;
            state.Complete(e);
        }

        private void GetWallCompleted(object sender, GetWallCompletedEventArgs e) 
        {
            var state = (ServiceCallState<Wall>)e.UserState;
            state.Complete(e, () => e.Result);
        }

        private void GetWallsCompleted(object sender, GetWallsCompletedEventArgs e) 
        {
            var state = (ServiceCallState<List<Wall>>)e.UserState;
            state.Complete(e, () => e.Result);
        }

        // ----------- BEGIN --------------------------------------------

        public void GetAdvert(int id, Action<Advert> callback)
        {
            this.serviceClient.GetAdvertAsync(id, new ServiceCallState<Advert>(callback));
        }

        public void CreateAdvert(Advert advert, Action<int?> callback)
        {
            this.serviceClient.CreateAdvertAsync(advert, new ServiceCallState<int?>(callback));
        }

        public void GetAdverts(Action<System.Collections.Generic.List<Advert>> callback)
        {
            this.serviceClient.GetAdvertsAsync(new ServiceCallState<List<Advert>>(callback));
        }

        public void GetAdvertStatus(int advertId, Action<AdvertStatus> callback)
        {
            this.serviceClient.GetAdvertStatusAsync(advertId, new ServiceCallState<AdvertStatus>(callback));
        }

        public void GetUsers(Action<System.Collections.Generic.List<User>> callback)
        {
            this.serviceClient.GetUsersAsync(new ServiceCallState<List<User>>(callback));
        }

        public void IsLoggedIn(Action<User> callback)
        {
            this.serviceClient.IsLoggedInAsync(new ServiceCallState<User>(callback));
        }

        public void Login(string login, string pass, Action<User> callback)
        {
            this.serviceClient.LoginAsync(login, pass, new ServiceCallState<User>(callback));
        }

        public void CreateUser(User user, Action<int?> callback)
        {
            this.serviceClient.CreateUserAsync(user, new ServiceCallState<int?>(callback));
        }

        public void UpdateUser(User user, Action callback)
        {
            this.serviceClient.UpdateUserAsync(user, new ServiceCallState(callback));
        }

        public void GetWalls(Action<List<Wall>> callback)
        {
            this.serviceClient.GetWallsAsync(new ServiceCallState<List<Wall>>(callback));
        }

        public void GetWall(int wallId, Action<Wall> callback)
        {
            this.serviceClient.GetWallAsync(wallId, new ServiceCallState<Wall>(callback));
        }
    }
}
