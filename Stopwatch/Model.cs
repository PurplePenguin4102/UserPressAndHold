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
        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public Model() => LatestStopwatchValue();

        public long ShownTime { get => stopwatch.ElapsedMilliseconds; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void StartTimer() => stopwatch.Restart();
        public void StopTimer() => stopwatch.Stop();
        public void ResetTimer() => stopwatch.Reset();

        private async void LatestStopwatchValue()
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
    }
}
