using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRT.Metrics
{
    /// <summary>
    /// This timer class can be used to test the performance of critical code blocks with
    /// low overhead and high precision. Sets of PerformanceCounters should be implemented
    /// as properties of a class inheriting from PerformanceCounters.
    /// </summary>

    public sealed class PerformanceCounter
    {
        private System.Diagnostics.Stopwatch Watch = new System.Diagnostics.Stopwatch();

        /// <summary>
        /// Optional, allows this to be easily emitted to a database, web page, etc.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Total run time (in ticks) of this timer instance so far (using Start() or Time()).
        /// </summary>
        public long TotalDuration => Watch.ElapsedTicks;

        /// <summary>
        /// Total number of times this timer has been *started* so far (using Start() or Time()).
        /// </summary>
        public int Count { get; private set; }

        // Utility read-only properties for display of statistics
        public long AverageDuration => Count == 0 ? 0 : TotalDuration / Count;
        public long AverageDurationMS => Count == 0 ? 0 : TotalDurationMS / Count;
        public long TotalDurationMS => Watch.ElapsedMilliseconds;



        // Properties to retrieve only the most recent result.
        public long LastDurationMS { get; private set; }
        public long LastDuration { get; private set; }



        public PerformanceCounter(string name = null)
        {
            this.Name = name;
        }

        private long _lastElapsedTicks;
        private long _lastElapsedMS;

        /// <summary>
        /// Starts the timer and increments the counter. 
        /// </summary>

        public void Start()
        {
            if (Watch.IsRunning) return;
            _lastElapsedTicks = Watch.ElapsedTicks;
            _lastElapsedMS = Watch.ElapsedMilliseconds;
            Watch.Start();
            Count++;
        }

        /// <summary>
        /// Adds the number of ticks since the most recent Start() call.
        /// </summary>
        public void Finish()
        {
            Watch.Stop();
            // The Math.Max call is needed to avoid timing issues with buggy BIOS or HAL implementations.
            LastDuration = Math.Max(0, Watch.ElapsedTicks - _lastElapsedTicks);
            LastDurationMS = Watch.ElapsedMilliseconds - _lastElapsedMS;
        }



        /// <summary>
        /// Alternative method to separate Start/Finish calls. Use only with complex blocks of code with
        /// few iterations, otherwise the overhead of passing the delegate action may be significant.
        /// Returns itself to allow for fluid calls.
        /// Usage:
        ///		myTimer.Time( () => { Dostuff; } );
        /// </summary>
        public PerformanceCounter Time(Action action)
        {
            Start();
            action();
            Finish();
            return this;
        }

        /// <summary>
        /// Resets everything but the name.
        /// </summary>
        public void Reset()
        {
            _lastElapsedTicks = 0;
            LastDuration = 0;
            LastDurationMS = 0;
            Count = 0;
            Watch.Reset();
        }
    }
}
