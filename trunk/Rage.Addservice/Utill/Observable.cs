using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Rage.Addservice.Utill;
using Rage.Addservice.Utils;

namespace Rage.Addservice.Utill
{

    public class Observable<T> : ObservableBase, IObservable<T>
    {
        private T value;

        public Observable()
        {
        }

        public Observable(T value)
        {
            this.value = value;
        }

        [Pure]
        public virtual T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (Equals(this.value, value))
                    return;

                var oldValue = this.value;
                OnValueChanging(oldValue, value);
                this.value = value;
                this.OnValueChanged(oldValue, this.value);
            }
        }

        protected virtual void OnValueChanging(T oldValue, T newValue) { }


        protected virtual void OnValueChanged(T oldValue, T newValue)
        {
            RaiseValueChanged();
        }

        protected void RaiseValueChanged()
        {
            this.OnPropertyChanged("Value");
        }

        public static implicit operator T(Observable<T> val)
        {
            return val.value;
        }

        public override string ToString()
        {
            return string.Format("Observable: {0}", this.value);
        }
    }
}