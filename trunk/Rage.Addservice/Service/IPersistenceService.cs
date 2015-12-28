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
using System.Collections.Generic;

namespace Rage.Addservice.Service
{
    public interface IPersistenceService
    {
        //advert
        void CreateAdvert(Advert advert, Action<int?> callback);
        void GetAdvert(int id, Action<Advert> callback);
        void GetAdverts(Action<List<Advert>> callback);
        void GetAdvertStatus(int advertId, Action<AdvertStatus> callback);
        
        //user
        void GetUsers(Action<List<User>> callback);
        void Login(string login, string pass, Action<User> callback);
        void CreateUser(User user, Action<int?> callback);
        void UpdateUser(User user, Action callback);
        void IsLoggedIn(Action<User> callback);

        //wall
        void  GetWalls(Action<List<Wall>> callback);
        void GetWall(int walId, Action<Wall> callback);

    }
}
