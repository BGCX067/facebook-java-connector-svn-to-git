﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17379
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnitTests.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IPersistenceService")]
    public interface IPersistenceService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersistenceService/GetUsers", ReplyAction="http://tempuri.org/IPersistenceService/GetUsersResponse")]
        Rage.Addservice.Domain.Model.User[] GetUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersistenceService/Login", ReplyAction="http://tempuri.org/IPersistenceService/LoginResponse")]
        Rage.Addservice.Domain.Model.User Login([System.ServiceModel.MessageParameterAttribute(Name="login")] string login1, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersistenceService/CreateUser", ReplyAction="http://tempuri.org/IPersistenceService/CreateUserResponse")]
        System.Nullable<int> CreateUser(Rage.Addservice.Domain.Model.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersistenceService/UpdateUser", ReplyAction="http://tempuri.org/IPersistenceService/UpdateUserResponse")]
        void UpdateUser(Rage.Addservice.Domain.Model.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersistenceService/IsLoggedIn", ReplyAction="http://tempuri.org/IPersistenceService/IsLoggedInResponse")]
        Rage.Addservice.Domain.Model.User IsLoggedIn();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersistenceService/GetAdverts", ReplyAction="http://tempuri.org/IPersistenceService/GetAdvertsResponse")]
        Rage.Addservice.Domain.Model.Advert[] GetAdverts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersistenceService/GetAdvert", ReplyAction="http://tempuri.org/IPersistenceService/GetAdvertResponse")]
        Rage.Addservice.Domain.Model.Advert GetAdvert(int advId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersistenceService/CreateAdvert", ReplyAction="http://tempuri.org/IPersistenceService/CreateAdvertResponse")]
        System.Nullable<int> CreateAdvert(Rage.Addservice.Domain.Model.Advert advert);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersistenceService/GetAdvertStatus", ReplyAction="http://tempuri.org/IPersistenceService/GetAdvertStatusResponse")]
        Rage.Addservice.Domain.Model.AdvertStatus GetAdvertStatus(int advId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersistenceService/GetWalls", ReplyAction="http://tempuri.org/IPersistenceService/GetWallsResponse")]
        Rage.Addservice.Domain.Model.Wall[] GetWalls();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersistenceService/GetWall", ReplyAction="http://tempuri.org/IPersistenceService/GetWallResponse")]
        Rage.Addservice.Domain.Model.Wall GetWall(int walId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPersistenceServiceChannel : UnitTests.ServiceReference1.IPersistenceService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PersistenceServiceClient : System.ServiceModel.ClientBase<UnitTests.ServiceReference1.IPersistenceService>, UnitTests.ServiceReference1.IPersistenceService {
        
        public PersistenceServiceClient() {
        }
        
        public PersistenceServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PersistenceServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersistenceServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersistenceServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Rage.Addservice.Domain.Model.User[] GetUsers() {
            return base.Channel.GetUsers();
        }
        
        public Rage.Addservice.Domain.Model.User Login(string login1, string pass) {
            return base.Channel.Login(login1, pass);
        }
        
        public System.Nullable<int> CreateUser(Rage.Addservice.Domain.Model.User user) {
            return base.Channel.CreateUser(user);
        }
        
        public void UpdateUser(Rage.Addservice.Domain.Model.User user) {
            base.Channel.UpdateUser(user);
        }
        
        public Rage.Addservice.Domain.Model.User IsLoggedIn() {
            return base.Channel.IsLoggedIn();
        }
        
        public Rage.Addservice.Domain.Model.Advert[] GetAdverts() {
            return base.Channel.GetAdverts();
        }
        
        public Rage.Addservice.Domain.Model.Advert GetAdvert(int advId) {
            return base.Channel.GetAdvert(advId);
        }
        
        public System.Nullable<int> CreateAdvert(Rage.Addservice.Domain.Model.Advert advert) {
            return base.Channel.CreateAdvert(advert);
        }
        
        public Rage.Addservice.Domain.Model.AdvertStatus GetAdvertStatus(int advId) {
            return base.Channel.GetAdvertStatus(advId);
        }
        
        public Rage.Addservice.Domain.Model.Wall[] GetWalls() {
            return base.Channel.GetWalls();
        }
        
        public Rage.Addservice.Domain.Model.Wall GetWall(int walId) {
            return base.Channel.GetWall(walId);
        }
    }
}