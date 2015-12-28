using System;
using System.IO;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using System.Text;
using System.Net;
using Microsoft.Practices.ServiceLocation;
using Rage.Addservice.Domain.Model;
using Rage.Addservice.Domain.Repositories;
using Rage.Addservice.Host.Session;

namespace Rage.Addservice.Host.PersistenceService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PersistenceService : IPersistenceService
    {


        public List<User> GetUsers()
        {
            var repository = ServiceLocator.Current.GetInstance<IUserRepository>();
            return repository.GetUsers();
        }

        public User Login(string login, string password)
        {
            var repository = ServiceLocator.Current.GetInstance<IUserRepository>();
            var user =  repository.Login(login, password);
            SessionUtil.AddUserToSession(user);
            return user;
        }

        public User IsLoggedIn() 
        {
            return SessionUtil.IsLoggedIn();
        }

        public int? CreateUser(User user)
        {
            var repository = ServiceLocator.Current.GetInstance<IUserRepository>();
            return repository.CreateUser(user);
        }

        public void UpdateUser(User user)
        {
            var repository = ServiceLocator.Current.GetInstance<IUserRepository>();
            repository.UpdateUser(user);
        }

        public List<Advert> GetAdverts()
        {
            var repository = ServiceLocator.Current.GetInstance<IAdvertRepository>();
            return repository.GetAdverts(SessionUtil.GetCurrentUserId());
        }

        public Advert GetAdvert(int advId)
        {
            var repository = ServiceLocator.Current.GetInstance<IAdvertRepository>();
            return repository.GetAdvert(advId);
        }

        public int? CreateAdvert(Advert advert)
        {
            if (advert.UseTwitter)
            {
                //var client = new TwitterWS.TwitterWSPortTypeClient("TwitterWSHttpSoap11Endpoint");
                //client.sendMsg(advert.Post);
            }
            var repository = ServiceLocator.Current.GetInstance<IAdvertRepository>();
            return repository.CreateAdvert(advert, SessionUtil.GetCurrentUserId());
        }

        public AdvertStatus GetAdvertStatus(int advertId)
        {
            var repository = ServiceLocator.Current.GetInstance<IAdvertRepository>();
            return repository.GetAdvertStatus(advertId);
        }

        public List<Wall> GetWalls()
        {
            var repository = ServiceLocator.Current.GetInstance<IWallRepository>();
            return repository.GetWalls();
        }

        public Wall GetWall(int wallId)
        {
            var repository = ServiceLocator.Current.GetInstance<IWallRepository>();
            return repository.GetWall(wallId);
        }
    }
}
