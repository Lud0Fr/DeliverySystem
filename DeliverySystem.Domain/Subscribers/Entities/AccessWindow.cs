using System;

namespace DeliverySystem.Domain.Subscribers
{
    public class AccessWindow
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        private AccessWindow() { }

        public AccessWindow(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
