using System;
using System.Windows;
using System.Reflection;

namespace Rage.Addservice.Infrastructure
{
    public class ViewLocator : IViewLocator
    {
        public UIElement Locate(string viewName)
        {
            var fullName = string.Format("{0}.{1}", ViewNames.ViewNamespace, viewName);

            var viewType = Assembly.GetExecutingAssembly().GetType(fullName);

            return (UIElement)Activator.CreateInstance(viewType);
        }

        public UIElement Locate(Type type)
        {
            return (UIElement)Activator.CreateInstance(type);
        }
    }
}
