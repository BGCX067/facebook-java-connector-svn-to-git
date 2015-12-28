using System;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel;
using System.Collections.Generic;

namespace Rage.Addservice.Infrastructure
{
    public class ObservableDelegateCommand<T> : DelegateCommand<T>
    {
        public ObservableDelegateCommand(Action<T> executeMethod)
            : base(executeMethod)
        {

        }

        public ObservableDelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod, params INotifyPropertyChanged[] dependecies)
            : base(executeMethod, canExecuteMethod)
        {
            foreach (var dependecy in dependecies)
            {
                dependecy.PropertyChanged += (e, s) => RaiseCanExecuteChanged();
            }
        }
    }

    public class ObservableDelegateCommand : DelegateCommand
    {
        public ObservableDelegateCommand(Action executeMethod)
            : base(executeMethod)
        {

        }

        public ObservableDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod, IEnumerable<INotifyPropertyChanged> dependecies)
            : base(executeMethod, canExecuteMethod)
        {
            foreach (var dependecy in dependecies)
            {
                dependecy.PropertyChanged += DependecyOnPropertyChanged();
            }

        }

        private PropertyChangedEventHandler DependecyOnPropertyChanged()
        {
            return (e, s) => RaiseCanExecuteChanged();
        }

        public ObservableDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod, params INotifyPropertyChanged[] dependecies)
            : this(executeMethod, canExecuteMethod, dependecies.ToList())
        {

        }

        public void RemoveDependency(INotifyPropertyChanged dependency)
        {
            dependency.PropertyChanged -= DependecyOnPropertyChanged();
        }

        public void AddDependency(INotifyPropertyChanged dependency)
        {
            dependency.PropertyChanged += DependecyOnPropertyChanged();
        }
    }

}
