using System.Diagnostics;

namespace LineDrawer.Common
{
    /// <summary>
    /// Timer helper class to get time in milisecond
    /// </summary>
    public class Timer
    {
        private Stopwatch _stopwatch;
        private double _lastUpdate; // max value of a double is positive 1.7976931348623157 E^308, 

        public Timer()
        {
            _stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Starts the internal timer
        /// </summary>
        public void Start()
        {
            _stopwatch.Start();
            _lastUpdate = 0;
        }


        /// <summary>
        /// Stops the internal timer
        /// </summary>
        public void Stop()
        {
            _stopwatch.Stop();
        }

        /// <summary>
        /// Updates the timer and returns time delta since last timer update
        /// </summary>
        /// <returns></returns>
        public double Update()
        {
            double now = ElapsedTime;
            double updateTime = now - _lastUpdate;
            _lastUpdate = now;
            return updateTime;
        }

        /// <summary>
        /// Returns elapsed time in milliseconds since timer started
        /// </summary>
        public double ElapsedTime
        {
            get
            {
                return _stopwatch.ElapsedMilliseconds * 0.001;
            }
        }
    }
}
