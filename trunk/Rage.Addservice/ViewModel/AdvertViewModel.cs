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
using Rage.Addservice.Service;
using Rage.Addservice.Domain.Model;
using Rage.Addservice.Utill;
using Microsoft.Practices.Prism.Commands;
using Rage.Addservice.ViewModel.Base;
using System.Windows.Media.Imaging;
using System.IO;
using Rage.Addservice.Helpers;
using Microsoft.Practices.ServiceLocation;
using Rage.Addservice.Infrastructure;

namespace Rage.Addservice.ViewModel
{
    public enum ContentType
    {
        Image,
        Video
    };

    public class AdvertViewModel : ViewModelBase
    {
        private Observable<string> name = new Observable<string>();
        private Observable<string> description = new Observable<string>();
        private Observable<string> post = new Observable<string>();
        private Observable<WallViewModel> wall = new Observable<WallViewModel>();
        private Observable<string> creationTime = new Observable<string>();
        private readonly Observable<int?> id = new Observable<int?>();
        private Observable<bool> useTwitter = new Observable<bool>(false);

        private Observable<BitmapImage> image = new Observable<BitmapImage>();

        public AdvertViewModel()
        { 
            
        }
        public AdvertViewModel(Advert advert) 
        {

            this.name.Value = advert.Name;
            this.description.Value = advert.Description;
            this.post.Value = advert.Post;
            this.wall.Value = new WallViewModel(advert.Wall);
            this.id.Value = advert.Id;
            this.creationTime.Value = advert.CreationTime.ToShortDateString() + " " + advert.CreationTime.ToShortTimeString();
            this.useTwitter.Value = advert.UseTwitter;
            
            if(advert.Attachment != null)
            {
                this.image = new Observable<BitmapImage>(new BitmapImage());
                var ms = new MemoryStream(advert.Attachment);
                this.image.Value.SetSource(ms);
            }
        }


        public Observable<string> Name 
        {
            get { return this.name; }
        }

        public Observable<string> Description
        {
            get { return this.description; }
        }

        public Observable<string> Post
        {
            get { return this.post; }
        }

        public Observable<WallViewModel> Wall
        {
            get { return this.wall; }
        }

        public Observable<int?> Id 
        {
            get { return this.id; }
        }

        public Observable<string> CreationTime 
        {
            get { return this.creationTime; }
        }

        public bool HasContent 
        {
            get { return this.image.Value != null; }
        }

        public Observable<bool> UseTwitter 
        {
            get { return this.useTwitter; }
            set { this.useTwitter.Value = value; }
        }

        public BitmapImage Image
        {
            get { return this.image.Value; }
            set 
            { 
                this.image.Value = value;
                this.OnPropertyChanged("Image");
                this.OnPropertyChanged("HasContent");
            }
        }

        public Advert GetModel() 
        {
            byte[] atachement = null;
            if (this.image.Value != null)
            {
                var stream = new MemoryStream();

                this.image.Value.Save(stream);
                atachement = stream.GetBuffer();
            }

            return new Advert()
            {
                Description = this.Description.Value,
                Id = null,
                Name = this.Name.Value,
                Post = this.Post.Value,
                Attachment = atachement,
                Attachment_Type = atachement == null ? "" : "IMAGE",
                CreationTime = DateTime.Now,
                UseTwitter = useTwitter.Value,
            };
        }

        public DelegateCommand OnShowWallCommand 
        {
            get { return new DelegateCommand(() => 
            { 
                ServiceLocator.Current.GetInstance<IViewConductor>().Show("WallDetailsPopup", this.wall.Value);
            }); 
            }
        }

    }
}
