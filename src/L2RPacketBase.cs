using System;
using System.Text;
using Kamael.Packets.ExtensionMethods;

namespace Kamael.Packets
{
    public partial class L2RPacketBase : IL2RPacket
    {
        /// <summary>
        /// The bytes
        /// </summary>
        public byte[] Bytes { get; set; }

        /// <summary>
        /// The index
        /// </summary>
        public int Index { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="L2RPacketBase"/> class.
        /// </summary>
        public L2RPacketBase()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="L2RPacketBase"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public L2RPacketBase(byte[] bytes)
        {
            Bytes = bytes;
            Index = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="L2RPacketBase"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        private L2RPacketBase(byte[] bytes, int index)
        {
            Bytes = bytes;
            Index = index;
        }

        /// <summary>
        /// Gets the remaining bytes.
        /// </summary>
        public int Remaining => Bytes.Length - Index;

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public L2RPacketBase Clone()
        {
            return new L2RPacketBase(Bytes, Index);
        }

        /// <summary>
        /// Reads the byte.
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            byte value = 0;

            if ((Bytes.Length - 1) >= (Index + 1))
            {
                value = Bytes[Index];
            }
            
            Index += 1;

            return value;
        }

        /// <summary>
        /// Reads the bytes.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public byte[] ReadBytes(int length)
        {
            byte[] value = new byte[length];

            if ((Bytes.Length - 1) >= (Index + length))
            {
                Array.Copy(Bytes, Index, value, 0, length);
            }
                
            Index += length;

            return value;
        }

        // TODO: Consider custom reader extensions for different types instead of embedding interpretation here.
        /// <summary>
        /// Reads the date.
        /// </summary>
        /// <returns></returns>
        public DateTime ReadDate()
        {
            long seconds = ReadInt64();
            if (seconds > 0)
            {
                return new DateTime(1970, 1, 1).AddSeconds(seconds /*- 18000 /*This adjusts timezone to EST. Server time is UTC-2 */);
            }
            else
            {
                return DateTime.MaxValue;
            }
        }

        /// <summary>
        /// Reads the int16.
        /// </summary>
        /// <returns></returns>
        public short ReadInt16()
        {
            short value = 0;

            if ((Bytes.Length - 1) >= (Index + 2))
            {
                value = BitConverter.ToInt16(Bytes, Index);
            }

            Index += 2;

            return value;
        }

        /// <summary>
        /// Reads the int32.
        /// </summary>
        /// <returns></returns>
        public int ReadInt32()
        {
            int value = 0;

            if ((Bytes.Length - 1) >= (Index + 4))
            {
                value = BitConverter.ToInt32(Bytes, Index);
            }
                
            Index += 4;

            return value;
        }

        /// <summary>
        /// Reads the int64.
        /// </summary>
        /// <returns></returns>
        public long ReadInt64()
        {
            long value = 0;

            if ((Bytes.Length - 1) >= (Index + 8))
            {
                value = BitConverter.ToInt64(Bytes, Index);
            }

            Index += 8;

            return value;
        }

        /// <summary>
        /// Reads the single.
        /// </summary>
        /// <returns></returns>
        public float ReadSingle()
        {
            float value = 0;

            if ((Bytes.Length - 1) >= (Index + 4))
            {
                value = BitConverter.ToSingle(Bytes, Index);
            }

            Index += 4;

            return value;
        }

        /// <summary>
        /// Reads the string.
        /// </summary>
        /// <returns></returns>
        public string ReadString()
        {
            string value = string.Empty;
            int length = ReadUInt16();

            if (length > 0)
            {
                if ((Bytes.Length - 1) >= (Index + length))
                {
                    value = Encoding.UTF8.GetString(Bytes, Index, length);
                }

                Index += length;
            }

            return value;
        }

        /// <summary>
        /// Reads the u int16.
        /// </summary>
        /// <returns></returns>
        public ushort ReadUInt16()
        {
            ushort value = 0;

            if ((Bytes.Length - 1) >= (Index + 2))
            {
                value = BitConverter.ToUInt16(Bytes, Index);
            }
                
            Index += 2;

            return value;
        }

        /// <summary>
        /// Reads the u int32.
        /// </summary>
        /// <returns></returns>
        public uint ReadUInt32()
        {
            uint value = 0;

            if ((Bytes.Length) - 1 >= (Index + 4))
            {
                value = BitConverter.ToUInt32(Bytes, Index);
            }
            
            Index += 4;

            return value;
        }

        /// <summary>
        /// Reads the u int64.
        /// </summary>
        /// <returns></returns>
        public ulong ReadUInt64()
        {
            ulong value = 0;

            if ((Bytes.Length) - 1 >= (Index + 8))
            {
                value = BitConverter.ToUInt64(Bytes, Index);
            }
                
            Index += 8;

            return value;
        }

        /// <summary>
        /// Sets the index.
        /// </summary>
        /// <param name="newindex">The newindex.</param>
        public void SetIndex(int newindex)
        {
            Index = newindex;
        }

        /// <summary>
        /// Skips the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        public void Skip(int count)
        {
            Index += count;
        }
    }
}