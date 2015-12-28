using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using Rage.Addservice.Infrastructure;

namespace Rage.Addservice.Infrastructure
{
    public class ViewConductor : IViewConductor
    {
        ViewLocator locator;

        public ViewConductor()
        {
            this.locator = new ViewLocator();
        }

        public void Show(string viewName)
        {
            var view = locator.Locate(viewName);

            var popup = view as IPopupView;
            if (popup != null)
            {
                popup.Show();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void Show<T>(string viewName, T context)
        {
            var view = locator.Locate(viewName);

            ShowView(view, context);
        }

        public void Show<T>(Type viewType, T context)
        {
            var view = locator.Locate(viewType);

            ShowView(view, context);
        }

        private void ShowView<T>(UIElement view, T context)
        {
            var popup = view as IPopupView;
            if (popup != null)
            {
                popup.Show();
            }
            else
            {
                throw new NotSupportedException();
            }


            var element = view as FrameworkElement;
            if (element != null)
            {
                element.DataContext = context;
            }
        }
    }
}
