using System;

namespace Kamael.Packets
{
    public partial class L2RPacketService
    {
        public class L2RPacketArrivalEventArgs : EventArgs
        {
            public ushort ID { get; set; }
            public IL2RPacket Packet { get; set; }
            public byte[] PacketBytes { get; set; }
        }
    }
}