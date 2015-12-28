using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using Rage.Addservice.Domain.Model;



namespace Rage.Addservice.Host.PersistenceService
{
    [ServiceContract]
    public interface IPersistenceService
    {

        //  USERS
        [OperationContract]     // po co get users ?
        List<User> GetUsers();

        [OperationContract]
        User Login(string login, string pass);

        [OperationContract]
        int? CreateUser(User user);

        [OperationContract]
        void UpdateUser(User user);

        [OperationContract]
        User IsLoggedIn();


        //  ADVERTS
        [OperationContract]
        List<Advert> GetAdverts();

        [OperationContract]
        Advert GetAdvert(int advId);

        [OperationContract]
        int? CreateAdvert(Advert advert);

        [OperationContract]
        AdvertStatus GetAdvertStatus(int advId);

        //  WALLS
        [OperationContract]
        List<Wall> GetWalls();

        [OperationContract]
        Wall GetWall(int walId);


    }
}
