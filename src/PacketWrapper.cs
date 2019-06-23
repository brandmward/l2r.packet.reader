using PacketDotNet;
using SharpPcap;

namespace Kamael.Packets
{
    public partial class L2RPacketService
    {
        public class PacketWrapper
        {
            public RawCapture p;

            public int Count { get; private set; }
            public PosixTimeval Timeval => p.Timeval;
            public LinkLayers LinkLayerType => p.LinkLayerType;
            public int Length => p.Data.Length;

            public PacketWrapper(int count, RawCapture p)
            {
                Count = count;
                this.p = p;
            }
        }
    }
}