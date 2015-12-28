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
using Rage.Addservice.ViewModel;

namespace Rage.Addservice.Views
{
    public partial class LoginPopup : IPopupView
    {
        public LoginPopup()
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

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.Context.OnLoginCommand.CanExecute())
                {
                    this.Context.OnLoginCommand.Execute();
                    this.OKButton_Click(this, null);
                }
            }
            else 
            {
                Context.Login.Value = this.textBox1.Text;
                Context.Password.Value = this.textBox2.Password;
            }
        }

        private UserViewModel Context 
        {
            get { return this.DataContext as UserViewModel; }
        }
    }
}

