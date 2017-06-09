using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StopwatchUI
{
    public class Model : INotifyPropertyChanged
    {
        public long ShownTime
        {
            get => stopwatch.ElapsedMilliseconds;
        }

        public Model() => LatestStopwatch();

        private async void LatestStopwatch()
        {
            while (true)
            {
                long oldTime = ShownTime;
                await Task.Delay(TimeSpan.FromMilliseconds(10));
                if (oldTime != ShownTime)
                    OnPropertyChanged(nameof(ShownTime));
            }
        }

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public event PropertyChangedEventHandler PropertyChanged;

        public void StartTimer()
        {
            stopwatch.Start();
        }

        public void StopTimer()
        {
            stopwatch.Stop();
        }

        public void ResetTimer()
        {
            stopwatch.Reset();
        }
    }
}
