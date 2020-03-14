using System;
using System.Threading;
using System.Windows.Threading;

namespace WpfApp
{
    public class UiDispatcher : IUiDispatcher
    {
        private readonly Dispatcher _dispatcher;

        public UiDispatcher()
        {
            _dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
        }

        public void RunSync(Action action)
        {
            if (CheckAccess())
            {
                action();
            }
            else
            {
                _dispatcher.Invoke(action, new object[0]);
            }
        }

        public void RunAsync(Action action)
        {
            if (CheckAccess())
            {
                action();
            }
            else
            {
                _dispatcher.BeginInvoke(action, new object[0]);
            }
        }

        public bool CheckAccess()
        {
            return _dispatcher.CheckAccess();
        }
    }
}
