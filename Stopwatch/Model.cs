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
        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public Model() => new Timer(UpdateUI, stopwatch, 10, 10);
        public long ShownTime { get => stopwatch.ElapsedMilliseconds; }
        private long oldTime = 0;
        private CoreDispatcher Dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
        public event PropertyChangedEventHandler PropertyChanged;
        public void StartTimer() => stopwatch.Restart();
        public void StopTimer() => stopwatch.Stop();
        public void ResetTimer() => stopwatch.Reset();

        private async void UpdateUI(object state)
        {
            if (oldTime != ShownTime)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => OnPropertyChanged(nameof(ShownTime)));
                oldTime = ShownTime;
            }
            
        }

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
