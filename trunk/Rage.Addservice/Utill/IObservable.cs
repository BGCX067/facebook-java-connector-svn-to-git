using System.ComponentModel;

namespace Rage.Addservice.Utill
{
    public interface IObservable<T> : INotifyPropertyChanged
    {
        T Value { get; }
    }
}