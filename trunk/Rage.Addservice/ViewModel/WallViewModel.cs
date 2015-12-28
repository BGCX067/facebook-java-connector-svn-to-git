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
using Rage.Addservice.ViewModel.Base;
using Rage.Addservice.Utill;
using System.Windows.Media.Imaging;
using Rage.Addservice.Domain.Model;
using System.IO;

namespace Rage.Addservice.ViewModel
{
    public class WallViewModel : ViewModelBase
    {
        private readonly Observable<long?> id = new Observable<long?>();
        private readonly Observable<string> name = new Observable<string>();
        private readonly BitmapImage image;
        private readonly byte[] imageBytes;
        private readonly Observable<string> description = new Observable<string>();

        public WallViewModel(Wall wall) 
        {
            this.id.Value = wall.Id.Value;
            this.name.Value = wall.Name;
            this.image = new BitmapImage();
            this.description.Value = wall.Description;
            this.imageBytes = wall.Image;

            var ms = new MemoryStream(wall.Image);
            this.image.SetSource(ms);
        }

        public Observable<long?> Id 
        {
            get { return this.id; }
        }

        public Observable<string> Name 
        {
            get { return this.name; }
        }

        public BitmapImage Image 
        {
            get { return this.image; }
        }

        public Observable<string> Description 
        {
            get { return this.description; }
        }

        public Wall GetModel() 
        {
            return new Wall()
            {
                Description = this.description,
                Id = this.id,
                Image = this.imageBytes,
                Name = this.name
            };
        }
    }
}
