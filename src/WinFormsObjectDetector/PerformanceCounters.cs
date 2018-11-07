using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SignalRT.Metrics
{
    /// <summary>
    /// Groups of performance timers should inherit from this, add their own Timer fields (ideally read-only)
    /// and reference them directly.
    /// 
     /// Note that a PerformanceTimers subclass can also have fields that are instances of *other* subclasses
    /// of PerformanceTimers -- the reflection calls will loop through them when emitting the list of timers.
    /// This allows a long list of timers to be grouped, and allows some composition of groups of timers to
    /// overcome C# limitations of single inheritance.
    /// </summary>
    public class PerformanceCounters
    {
        /// <summary>
        /// Intended to be used when this instance is a property of another PerformanceCounters instance,
        /// allows all of the timers to be emitted with a common name prefix.
        /// </summary>
        public string NamePrefix { get; set; } = string.Empty;

        public void Reset()
        {
            foreach (var timer in Timers) timer.Reset();
        }


        /// <summary>
        /// Returns a list of all PerformanceCounter fields that are either members of this class
        /// or are fields of PerformanceCounter-derived fields. This recurses, allowing PerformanceCounters
        /// subclasses to have PerformanceCounters fields, which can then also have PerformanceCounters
        /// fields, etc. Implemented as IReadOnlyCollection<> because AFAIK C# doesn't currently 
        /// support multiple yield return statements to allow for an IEnumerable<> implementation.
        /// </summary>
        public IReadOnlyCollection<PerformanceCounter> Timers
        {
            get
            {
                // Initialize the list with the PerformanceTimer fields
                var timers = new List<PerformanceCounter>(MemberTimers);
                
                // Loop through PerformanceTimers-derived fields to add additional timers from them.
                var listsOfTimers = from timerGroup in MemberTimerGroups select timerGroup.Timers;

                foreach (var otherTimerList in listsOfTimers)
                {
                    timers.AddRange(otherTimerList);
                }

                // Return a collection that the caller can't modify.
                return timers.AsReadOnly();
            }
        }


        /// <summary>
        /// Private only, returns the fields of this class that are of type PerformanceCounter
        /// </summary>
        private IEnumerable<PerformanceCounter> MemberTimers
        {
            get
            {
                return this.GetType().GetTypeInfo().DeclaredFields
                    .Where(fieldinfo => fieldinfo.FieldType == typeof(PerformanceCounter))
                    .Select(fieldinfo => (PerformanceCounter)fieldinfo.GetValue(this));
            }
        }

        /// <summary>
        /// Private only, returns the fields of this class that are *subclasses* of type PerformanceTimers.
        /// </summary>
        private IEnumerable<PerformanceCounters> MemberTimerGroups
        {
            get
            {
                return this.GetType().GetTypeInfo().DeclaredFields
                    .Where(fieldinfo => typeof(PerformanceCounters).IsAssignableFrom(fieldinfo.FieldType))
                    .Select(fieldinfo => (PerformanceCounters)fieldinfo.GetValue(this));
            }
        }
    }
}
