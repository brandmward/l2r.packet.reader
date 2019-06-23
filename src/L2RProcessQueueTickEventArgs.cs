using System;

namespace Kamael.Packets
{
    public partial class L2RPacketService
    {
        /// <summary>
        /// Event args that provide queue count information.
        /// </summary>
        public class L2RProcessQueueTickEventArgs : EventArgs
        {
            public int QueueCount { get; private set; }

            public L2RProcessQueueTickEventArgs(int queueCount)
            {
                QueueCount = queueCount;
            }
        }
    }
}