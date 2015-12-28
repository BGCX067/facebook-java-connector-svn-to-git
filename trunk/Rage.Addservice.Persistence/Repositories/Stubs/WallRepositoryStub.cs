using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage.Addservice.Domain.Repositories;
using Rage.Addservice.Domain.Model;
using System.Drawing;

namespace Rage.Addservice.Persistence.Repositories.Stubs
{
    public class WallRepositoryStub : IWallRepository
    {
        private List<Wall> walls = new List<Wall>()
            {
                new Wall()
                {
                    Description = "Wszyscy zwolennicy Kręcina jednoczmy się.",
                    Id = 1,
                    Image = ImageToByteConverter.ConvertToByteArray(new Bitmap(Rage.Addservice.Persistence.Properties.Resources._73982)),
                    Name = "Kręcina supporters!"
                },
                new Wall()
                {
                    Description = "Fuck techno love KoRn - polski funpage.",
                    Id = 2,
                    Image = ImageToByteConverter.ConvertToByteArray(new Bitmap(Rage.Addservice.Persistence.Properties.Resources.korn)),
                    Name = "KoRn"
                },
                new Wall()
                {
                    Description = "This is sample text for description. Do not copy this.",
                    Id = 3,
                    Image = ImageToByteConverter.ConvertToByteArray(new Bitmap(Rage.Addservice.Persistence.Properties.Resources.do_something_logo_200)),
                    Name = "Do something"
                },
                new Wall()
                {
                    Description = "Funpage kibicow i sympatyków krakowskiego klubu sportowego Cracovia Kraków.",
                    Id = 4,
                    Image = ImageToByteConverter.ConvertToByteArray(new Bitmap(Rage.Addservice.Persistence.Properties.Resources.Cracovia_Kraków_herb)),
                    Name = "Cracovia Kraków"
                },
                new Wall()
                {
                    Description = "Funpage poswiecony zespołowi Metallica. Zapraszamy do polupienia strony.",
                    Id = 5,
                    Image = ImageToByteConverter.ConvertToByteArray(new Bitmap(Rage.Addservice.Persistence.Properties.Resources.Metal1)),
                    Name = "Metallica Polska"
                },
                new Wall()
                {
                    Description = "Juz nie wiem co mam wymyslic wiec wrzcam logo jakiejs drukarki czy cos takiego. Pozdarwiam i zapraszam do lajkowania.",
                    Id = 6,
                    Image = ImageToByteConverter.ConvertToByteArray(new Bitmap(Rage.Addservice.Persistence.Properties.Resources.PrintWhatYouLike200x200)),
                    Name = "Print what You like."
                }
            };


        public List<Wall> GetWalls()
        {
            return this.walls;
        }

        public Wall GetWall(long walId)
        {
            try
            {
                return walls.Where(w => w.Id == walId).ToArray()[0];
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
