using System;

namespace Rage.Addservice.Infrastructure
{
    public interface IViewConductor
    {
        void Show(string viewName);
        void Show<T>(string viewName, T context);
        void Show<T>(Type viewType, T context);
    }
}
