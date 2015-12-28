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

namespace Rage.Addservice.Infrastructure
{
    public class UserManager : IUserManager
    {

        private User user;

        public User User
        {
            get
            {
                return user;
            }
            set
            {
                this.user = value;
            }
        }
    }
}
