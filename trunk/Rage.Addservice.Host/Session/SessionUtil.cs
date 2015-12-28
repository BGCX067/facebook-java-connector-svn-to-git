using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rage.Addservice.Domain.Model;

namespace Rage.Addservice.Host.Session
{

    public class SessionUtil
    {
        const string userKey = "CURRENT_USER";
        public static void AddUserToSession(User user) 
        {
            HttpContext.Current.Items.Add(userKey, user);
        }

        public static User IsLoggedIn() 
        {
            User user = null;
            try
            {
                user = (User)HttpContext.Current.Items[userKey];
            }
            catch(Exception e)
            {
                return null;
            }

            return user;
        }

        public static int GetCurrentUserId() 
        {
            int id;
            try
            {
                id = ((User)HttpContext.Current.Items[userKey]).Id.Value;
            }
            catch (Exception e) 
            {
                //User Not Logged In
                return 4;
            }
            return 4;
        }
    }
}