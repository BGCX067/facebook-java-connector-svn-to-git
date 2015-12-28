using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage.Addservice.Domain.Repositories;
using Rage.Addservice.Domain.Model;

namespace Rage.Addservice.Persistence.Repositories.Stubs
{
    public class UserRepositoryStub : IUserRepository
    {
        private int UserId 
        {
            get 
            {
                return users.Max(u => u.Id).Value + 1;
            }
        }
        
        private List<User> users = new List<User>()
        {
            new User()
            {
                Email = "tomek@asd.pl",
                Id = 1,
                Login = "tomek",
                Name = "Tomek Sowiński",
                Password = "tomek"
            }
        };

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public User Login(string login, string pass)
        {
            return users[0];
        }

        public int? CreateUser(User user)
        {
            user.Id = UserId;
            this.users.Add(user);

            return user.Id;
        }

        public void UpdateUser(User user)
        {
            var userRef = this.users.First(u => u.Id == user.Id);

            this.users.Remove(userRef);

            this.users.Add(user);
        }
    }
}
