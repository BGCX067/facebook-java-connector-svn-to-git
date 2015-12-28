using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Rage.Addservice.ViewModel;
using System.Linq;

namespace Rage.Addservice.Controls
{
	public partial class AppTabControl : UserControl
	{
		public AppTabControl()
		{
			// Required to initialize variables
			InitializeComponent();
		}

        private void SeeDetails(object sender, RoutedEventArgs e)
        {
            this.Context.SeeDetails();
        }

        private void SeeWallDetails(object sender, RoutedEventArgs e)
        {
            this.Context.SeeWallDetails();
        }

        private void SeeSelectedWallDetails(object sender, RoutedEventArgs e)
        {
            this.Context.SeeSelectedWallDetails();
        }

        private AdvertsContainerViewModel Context 
        {
            get { return this.DataContext as AdvertsContainerViewModel; }
        }

        private void List_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                VisualTreeHelper.FindElementsInHostCoordinates(e.GetPosition(null), (sender as ListBox)).OfType<ListBoxItem>().First().IsSelected = true;
            }
            catch
            {
                e.Handled = true;
            }
        }

        private void List_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }   
	}
}