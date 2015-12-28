using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Rage.Addservice.Infrastructure;
using System.Windows.Media.Imaging;
using Rage.Addservice.ViewModel;
using System.IO;

namespace Rage.Addservice.Views
{
    public partial class NewAdvertPopup : IPopupView
    {
        public NewAdvertPopup()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();

            dlg.Filter = "Media files (.jpg .bmp .png)|*.jpg;*.bmp;*.png";
            dlg.FilterIndex = 1;

            dlg.Multiselect = false;

            if (dlg.ShowDialog() == true)
            {
                var image = new BitmapImage();
                image.SetSource(dlg.File.OpenRead());

                this.Context.EditableItem.Image = image;
            }
        }

        private AdvertsContainerViewModel Context 
        {
            get { return this.DataContext as AdvertsContainerViewModel; }
        }
    }
}

