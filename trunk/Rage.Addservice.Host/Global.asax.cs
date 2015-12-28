using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Rage.Addservice.Host.PersistenceService;
using Rage.Addservice.Persistence;
using System.Drawing;
using Rage.Addservice.Domain.Repositories;
using Microsoft.Practices.ServiceLocation;
using Rage.Addservice.Persistence.Repositories;

namespace Rage.Addservice.Host
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            //var image1 = ImageToByteConverter.ConvertToByteArray(new Bitmap(Addservice.Host.Properties.Resources.memoryclear));
            //var image2 = ImageToByteConverter.ConvertToByteArray(new Bitmap(Addservice.Host.Properties.Resources.politechnikakrk));

            //WallRepository temp = new WallRepository();
            //temp.InsertImage(108986722465277, image1);
            //temp.InsertImage(244376999003426, image2);

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}