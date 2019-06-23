using SharpPcap;
using System;

namespace Kamael.Packets
{
    public partial class L2RPacketService
    {
        /// <summary>
        /// Event argument wrapper for exceptions caught while reading packets. 
        /// </summary>
        public class L2RPacketErrorEventArgs : EventArgs
        {
            /// <summary>
            /// The excpetion raised.
            /// </summary>
            public Exception Exception { get; private set; }

            /// <summary>
            /// The packet that raised the error. Can be null.
            /// </summary>
            public RawCapture Packet { get; private set; }

            public L2RPacketErrorEventArgs(Exception ex)
                : this(ex, null)
            {
            }

            public L2RPacketErrorEventArgs(Exception ex, RawCapture packet)
            {
                Exception = ex;
                Packet = packet;
            }
        }
    }
}