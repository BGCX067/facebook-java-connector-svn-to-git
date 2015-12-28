using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage.Addservice.Domain.Repositories;
using Rage.Addservice.Domain.Model;
using System.Drawing;
using System.IO;

namespace Rage.Addservice.Persistence.Repositories.Stubs
{
    public class AdvertRepositoryStub : IAdvertRepository
    {
        private static IWallRepository wallRepo = new WallRepositoryStub();

        private List<Advert> adverts = new List<Advert>()
        {
            new Advert()
            {
                Id = 1,
                Name = "Gilette 6",
                Attachment_Type = "IMAGE",
                Attachment = ImageToByteConverter.ConvertToByteArray(new Bitmap(Rage.Addservice.Persistence.Properties.Resources.ra_200_200_n_473002xeMX_krzysztof_ibisz_anna_nowakibisz_olewa_syna)),
                Description = "Gilette 6 - reklama pierwsza.",
                Post = "Ktoś z was kiedykolwiek uzywał brzytwy? To już nie uzyje! Diś mamy Gilette 6.",
                Wall = wallRepo.GetWall(2),
                CreationTime = DateTime.Now
            },
            new Advert()
            {
                Id = 2,
                Name = "Euro 2012",
                Attachment_Type = "IMAGE",
                Attachment = ImageToByteConverter.ConvertToByteArray(new Bitmap(Rage.Addservice.Persistence.Properties.Resources.euro_2012_0)),
                Description = "Euro 2012 - reklama nr 3",
                Post = "A Wy jak myslicie, kto będzie pierwszym strzelcem gola w tegorocznym turnieju? Albo inaczej.. ile goli strzeli Lewy z Grecja?",
                Wall = wallRepo.GetWall(4),
                CreationTime = DateTime.Now,
                UseTwitter = true
            },
            new Advert()
            {
                Id = 3,
                Name = "Lewandowski",
                Attachment_Type = "IMAGE",
                Attachment = ImageToByteConverter.ConvertToByteArray(new Bitmap(Rage.Addservice.Persistence.Properties.Resources.euro_2012_0)),
                Description = "Euro 2012 - reklama nr 3",
                Post = "A Wy jak myslicie, kto będzie pierwszym strzelcem gola w tegorocznym turnieju? Albo inaczej.. ile goli strzeli Lewy z Grecja?",
                Wall = wallRepo.GetWall(3),
                CreationTime = DateTime.Now,
                UseTwitter = true
            },
        };

        private static int IdGenerator = 4;

        public int? CreateAdvert(Advert advert, int userId)
        {
            advert.Id = IdGenerator++;

            this.adverts.Add(advert);
            

            return advert.Id;
        }

        public AdvertStatus GetAdvertStatus(int advertId) 
        {
            return new AdvertStatus();
        }

        public Advert GetAdvert(int id)
        {
            try 
            {
                return this.adverts.Where(a => a.Id == id).ToArray()[0];
            }
            catch
            {
                return null;
            }
        }


        public List<Advert> GetAdverts(int userId)
        {
            return adverts;
            //return this.adverts.Select(a => 
            //{
            //    return new Advert()
            //    {
            //        Description = a.Description,
            //        Id = a.Id,
            //        Name = a.Name,
            //        Post = a.Post,
            //        Wall = a.Wall
            //    };
            //}).ToList();
        }
    }
}
