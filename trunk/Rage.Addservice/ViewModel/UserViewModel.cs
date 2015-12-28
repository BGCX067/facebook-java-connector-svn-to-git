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
using Rage.Addservice.Infrastructure;
using Rage.Addservice.Domain.Model;
using Microsoft.Practices.Prism.Commands;
using Rage.Addservice.Utill;
using Microsoft.Practices.ServiceLocation;

namespace Rage.Addservice.ViewModel
{
    public class UserViewModel
    {
        private readonly IPersistenceService persistenceService;
        private readonly Action onUserLoggedIn;
        private readonly IViewConductor viewConductor;

        private readonly ObservableDelegateCommand onLoginCommand;
        private readonly DelegateCommand onCancelLoginCommand;

        private readonly DelegateCommand onRegisterCommand;
        private readonly DelegateCommand onCancelRegisterCommand;

        private int id;
        private Observable<string> name = new Observable<string>("");
        private Observable<string> login = new Observable<string>("");
        private Observable<string> password = new Observable<string>("");
        private Observable<string> email = new Observable<string>(""); 

        public UserViewModel(IPersistenceService persistenceService, Action OnUserLoggedIn, IViewConductor viewConductor) 
        {
            this.persistenceService = persistenceService;
            this.viewConductor = viewConductor;
            this.onUserLoggedIn = OnUserLoggedIn;

            onLoginCommand = new ObservableDelegateCommand(OnLogin, () => { return (this.password.Value != "") && (this.login.Value != ""); }, this.password, this.login);
            onCancelLoginCommand = new DelegateCommand(OnCancelLogin);
            onRegisterCommand = new DelegateCommand(OnRegister);
            onCancelRegisterCommand = new DelegateCommand(OnCancelRegister);

        }

        private void Initialize(User user) 
        {
            this.email.Value = user.Email;
            this.name.Value = user.Name;
            this.id = user.Id.Value;
        }

        private void OnLogin() 
        {
            this.persistenceService.Login(this.login.Value, this.password.Value, OnLoginCompleted);
        }

        private void OnLoginCompleted(User user) 
        {
            if (user == null)
            {
                this.Error.Value = "Bad username or password";
                viewConductor.Show("LoginPopup", this);
            }
            else 
            {
                this.Initialize(user);
                var userManager = ServiceLocator.Current.GetInstance<IUserManager>();
                userManager.User = user;

                onUserLoggedIn();
            }
        }

        private User GetModel() 
        {
            return new User()
            {
                Email = this.email.Value,
                Login = this.login.Value,
                Password = this.password.Value,
                Name = this.name.Value,
                Id = this.id
            };
        }

        private void OnCancelLogin() 
        {

        }

        private void OnRegister() 
        {
            persistenceService.CreateUser(GetModel(), OnRegisterCompleted);
        }

        private void OnRegisterCompleted(int? id) 
        {
            this.id = id.Value;

            var userManager = ServiceLocator.Current.GetInstance<IUserManager>();
            userManager.User = GetModel();

            onUserLoggedIn();
        }

        private void OnCancelRegister() 
        {

        }

        public void LogMeIn() 
        {
            this.persistenceService.IsLoggedIn(OnIsLoggedInCompleted);
        }

        private void OnIsLoggedInCompleted(User user) 
        {
            if (user == null)
            {
                viewConductor.Show("LoginPopup", this);
            }
            else 
            {
                this.Initialize(user);
                var userManager = ServiceLocator.Current.GetInstance<IUserManager>();
                userManager.User = user;

                onUserLoggedIn();
            }
        }
  
        
        public int Id        
        {
            get { return id; }
            set { id = value; }
        }

        public Observable<string> Name      
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public Observable<string> Login     
        {
            get { return this.login; }
            set { this.login = value; }
        }

        public Observable<string> Password  
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public Observable<string> Email     
        {
            get { return this.email; }
            set { this.email = value; }
        }

        private Observable<string> error = new Observable<string>("");

        public Observable<string> Error 
        {
            get { return this.error; }
        }

        public DelegateCommand OnLoginCommand 
        {
            get { return this.onLoginCommand; }
        }

        public DelegateCommand OnCancelLoginCommand 
        {
            get { return this.onCancelLoginCommand; }
        }

        public DelegateCommand OnRegisterCommand 
        {
            get { return this.onRegisterCommand; }
        }

        public DelegateCommand OnCancelRegisterCommand 
        {
            get { return this.onCancelRegisterCommand; }
        }

        public DelegateCommand OnSignUpCommand
        {
            get { return new DelegateCommand(() => 
            {
                this.password.Value = "";
                this.login.Value = "";
                this.viewConductor.Show("RegisterUserPopup", this);
            }); }
        }


    }
}
