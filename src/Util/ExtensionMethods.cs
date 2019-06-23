using System;
using System.Text;

namespace Kamael.Packets.ExtensionMethods
{
    public static class MyExtensions
    {
        

        /// <summary>
        /// Reads the byte.
        /// </summary>
        /// <returns></returns>
        public static byte ReadByte(this IL2RPacket packet)
        {
            byte value = 0;

            if ((packet.Bytes.Length) - 1 > (packet.Index + 1))
            {
                value = packet.Bytes[packet.Index];
            }
            
            packet.Index += 1;

            return value;
        }

        /// <summary>
        /// Reads the bytes.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static byte[] ReadBytes(this IL2RPacket packet, int length)
        {
            byte[] value = new byte[length];

            if ((packet.Bytes.Length - 1) >= (packet.Index + length))
            {
                Array.Copy(packet.Bytes, packet.Index, value, 0, length);
            }

            packet.Index += length;

            return value;
        }

        /// <summary>
        /// Reads the int16.
        /// </summary>
        /// <returns></returns>
        public static short ReadInt16(this IL2RPacket packet)
        {
            short value = 0;

            if ((packet.Bytes.Length - 1) > (packet.Index + 2))
            {
                value = BitConverter.ToInt16(packet.Bytes, packet.Index);
            }

            packet.Index += 2;

            return value;
        }

        /// <summary>
        /// Reads the int32.
        /// </summary>
        /// <returns></returns>
        public static int ReadInt32(this IL2RPacket packet)
        {
            int value = 0;

            if ((packet.Bytes.Length - 1) > (packet.Index + 4))
            {
                value = BitConverter.ToInt32(packet.Bytes, packet.Index);
            }

            packet.Index += 4;

            return value;
        }

        /// <summary>
        /// Reads the int64.
        /// </summary>
        /// <returns></returns>
        public static long ReadInt64(this IL2RPacket packet)
        {
            long value = 0;

            if ((packet.Bytes.Length - 1) > (packet.Index + 8))
            {
                value = BitConverter.ToInt64(packet.Bytes, packet.Index);
            }

            packet.Index += 8;

            return value;
        }

        /// <summary>
        /// Reads the single.
        /// </summary>
        /// <returns></returns>
        public static float ReadSingle(this IL2RPacket packet)
        {
            float value = 0;

            if ((packet.Bytes.Length - 1) > (packet.Index + 4))
            {
                value = BitConverter.ToSingle(packet.Bytes, packet.Index);
            }

            packet.Index += 4;

            return value;
        }

        /// <summary>
        /// Reads the string.
        /// </summary>
        /// <returns></returns>
        public static string ReadString(this IL2RPacket packet)
        {
            string value = string.Empty;

            //ushort len = BitConverter.ToUInt16(packet.Bytes, packet.Index);
            //packet.Index += 2;

            int length = ReadUInt16(packet);

            if (length > 0)
            {
                if ((packet.Bytes.Length - 1) >= (packet.Index + length))
                {
                    value = Encoding.UTF8.GetString(packet.Bytes, packet.Index, length);
                }
                
                packet.Index += length;
            }

            return value;
        }

        /// <summary>
        /// Reads the u int16.
        /// </summary>
        /// <returns></returns>
        public static ushort ReadUInt16(this IL2RPacket packet)
        {
            ushort value = 0;

            if ((packet.Bytes.Length - 1) > (packet.Index + 2))
            {
                value = BitConverter.ToUInt16(packet.Bytes, packet.Index);
            }

            packet.Index += 2;

            return value;
        }

        /// <summary>
        /// Reads the u int32.
        /// </summary>
        /// <returns></returns>
        public static uint ReadUInt32(this IL2RPacket packet)
        {
            uint value = 0;

            if ((packet.Bytes.Length - 1) > (packet.Index + 4))
            {
                value = BitConverter.ToUInt32(packet.Bytes, packet.Index);
            }

            packet.Index += 4;

            return value;
        }

        /// <summary>
        /// Reads the u int64.
        /// </summary>
        /// <returns></returns>
        public static ulong ReadUInt64(this IL2RPacket packet)
        {
            ulong value = 0;

            if ((packet.Bytes.Length - 1) > (packet.Index + 8))
            {
                value = BitConverter.ToUInt64(packet.Bytes, packet.Index);
            }

            packet.Index += 8;

            return value;
        }
        

        /// <summary>
        /// Skips the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        public static void Skip(this IL2RPacket packet, int count)
        {
            packet.Index += count;
        }
    }
}