namespace Kamael.Packets
{
    /// <summary>
    /// Represents a packet received from L2R
    /// </summary>
    public class L2RPacket : L2RPacketBase, IL2RPacket
    {
        public L2RPacket()
        {
        }

        public L2RPacket(byte[] bytes)
            : this(bytes, 0)
        {
        }

        public L2RPacket(byte[] bytes, int index)
        {
            Bytes = bytes;
            Index = index;
        }
    }
}