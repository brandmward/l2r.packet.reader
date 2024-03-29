﻿namespace Kamael.Packets.Items
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Kamael.Packets.IL2RPacket" />
    public class PacketSimpleEquipment : IL2RPacket
    {
        public byte[] Bytes { get; set; }
        public int Index { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketSimpleEquipment"/> class.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public PacketSimpleEquipment(IL2RPacket Packet)
        {
            L2RPacket packet = (L2RPacket)Packet;

            ItemInfoID = packet.ReadUInt32();
            EnchantLevel = packet.ReadByte();
            byte tmpUnk = packet.ReadByte();
        }

        /// <summary>
        /// Gets or sets the enchant level.
        /// </summary>
        /// <value>
        /// The enchant level.
        /// </value>
        private byte EnchantLevel { get; set; }

        /// <summary>
        /// Gets or sets the item information identifier.
        /// </summary>
        /// <value>
        /// The item information identifier.
        /// </value>
        private uint ItemInfoID { get; set; }
    }
}