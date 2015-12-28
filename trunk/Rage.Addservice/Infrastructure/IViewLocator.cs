using System;
using System.Windows;

namespace Rage.Addservice.Infrastructure
{
    public interface IViewLocator
    {
        UIElement Locate(string viewName);
        UIElement Locate(Type type);
    }
}
