using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ServiceModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Windows.UI.Xaml;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace StopwatchUI
{
    public class Model : INotifyPropertyChanged
    {
        // the worker object
        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        // we need this guy to compare to stopwatch.ElapsedMillis
        private long oldTime = 0;

        /// <summary>
        /// Starts a timer to update UI thread with stopwatch value
        /// </summary>
        public Model() => new Timer(UpdateUI, stopwatch, 10, 10);

        /// <summary>
        /// The stopwatch's current value in milliseconds
        /// </summary>
        public long ShownTime { get => stopwatch.ElapsedMilliseconds; }

        // we keep this guy so that we can notify the UI from the timer thread
        private CoreDispatcher Dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;

        // implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // wrappers for stopwatch functionality
        public void StartTimer() => stopwatch.Restart();
        public void StopTimer() => stopwatch.Stop();
        public void ResetTimer() => stopwatch.Reset();

        /// <summary>
        /// Our timer callback. Compares the stopwatch with the stored time and updates UI thread when different
        /// </summary>
        private async void UpdateUI(object state)
        {
            if (oldTime != ShownTime)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => OnPropertyChanged(nameof(ShownTime)));
                oldTime = ShownTime;
            }
        }

        // wrap the event handler with this utility. Don't need to populate if called from a property
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
