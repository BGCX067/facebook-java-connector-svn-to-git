using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage.Addservice.Domain.Model;

namespace Rage.Addservice.Domain.Repositories
{
    public interface IAdvertRepository
    {
        int? CreateAdvert(Advert advert, int userId);
        List<Advert> GetAdverts(int userId);
        Advert GetAdvert(int id);
        AdvertStatus GetAdvertStatus(int advertId);
    }
}
