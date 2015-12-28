using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage.Addservice.Domain.Model;

namespace Rage.Addservice.Domain.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers(); 

        User Login(string login, string pass);

        int? CreateUser(User user);

        void UpdateUser(User user);
    }
}
