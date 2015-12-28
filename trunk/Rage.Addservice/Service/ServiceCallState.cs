using System;
using System.ComponentModel;
using Rage.Addservice.Utill;

namespace Rage.Addservice.Services
{

    public class ServiceCallState
    {
        private readonly Action callback;
        private readonly Observable<bool> loadingStatus;

        public ServiceCallState(Action callback)
            : this(callback, null)
        {
        }

        public ServiceCallState(Action callback, Observable<bool> loadingStatus)
        {
            this.callback = callback;
            this.loadingStatus = loadingStatus;

            if (this.loadingStatus != null)
            {
                this.loadingStatus.Value = true;
            }
        }

        public void Complete(AsyncCompletedEventArgs e)
        {
            if (this.loadingStatus != null)
            {
                this.loadingStatus.Value = false;
            }

            if (this.callback != null)
            {
                this.callback();
            }
        }
    }

    public class ServiceCallState<T>
    {
        private readonly Action<T> callback;
        private readonly Observable<bool> loadingStatus;

        public ServiceCallState(Action<T> callback)
        {
            this.callback = callback;
        }

        public ServiceCallState(Action<T> callback, Observable<bool> loadingStatus)
        {
            this.callback = callback;
            this.loadingStatus = loadingStatus;

            if (this.loadingStatus != null)
            {
                this.loadingStatus.Value = true;
            }
        }

        public void Complete(AsyncCompletedEventArgs e, Func<T> result)
        {
            if (this.loadingStatus != null)
            {
                this.loadingStatus.Value = false;
            }

            if (result == null)
            {
                result = () => default(T);
            }

            if (this.callback != null)
            {
                this.callback(result());
            }
        }
    }
}
