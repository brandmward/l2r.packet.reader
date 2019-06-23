namespace Kamael.Packets
{
    public interface IL2RPacket
    {
        byte[] Bytes { get; set; }

        int Index { get; set; }
    }
}