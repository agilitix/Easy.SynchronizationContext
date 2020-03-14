using System;
using System.Threading;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUiDispatcher _dispacther;

        public MainWindow()
        {
            InitializeComponent();
            _dispacther = new UiDispatcher();
        }

        private void ShowTime()
        {
            int thId = Thread.CurrentThread.ManagedThreadId;

            for (int i = 0; i < 20; ++i)
            {
                // Update a text box in the UI thread.
                _dispacther.RunSync(() => { textBox.Text = DateTime.Now.ToString($"{thId} - MM/dd/yyyy HH:mm:ss"); });
                Thread.Sleep(1000);
            }
        }

        private void Button_ShowTime(object sender, RoutedEventArgs e)
        {
            // Start new thread to show how to update the UI from a worker thread.
            Thread th = new Thread(ShowTime);
            th.IsBackground = true; // The thread terminates immediately if the app/UI is closed.
            th.Start();
        }

        private void Button_NewWindow(object sender, RoutedEventArgs e)
        {
            Thread windowThread = new Thread(() =>
                                             {
                                                 Window w = new MainWindow();
                                                 w.Show();
                                                 w.Closed += (sender2, e2) => w.Dispatcher.InvokeShutdown();

                                                 // Start pumping messages.
                                                 System.Windows.Threading.Dispatcher.Run();
                                             });

            // Start new UI thread with its own message pump.
            windowThread.SetApartmentState(ApartmentState.STA);
            windowThread.IsBackground = true;
            windowThread.Start();
        }
    }
}
