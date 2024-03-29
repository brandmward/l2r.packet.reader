﻿namespace Kamael.Packets.Status
{
    /// <summary>
    ///
    /// </summary>
    public class PacketPartyEmblem : IL2RPacket
    {
        public byte[] Bytes { get; set; }
        public int Index { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketPartyEmblem"/> class.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public PacketPartyEmblem(IL2RPacket Packet)
        {
            L2RPacket packet = (L2RPacket)Packet;

            PartyName = packet.ReadString();
            Bool = packet.ReadByte();
        }

        /// <summary>
        /// Gets or sets the bool.
        /// </summary>
        /// <value>
        /// The bool.
        /// </value>
        private byte Bool { get; set; }

        /// <summary>
        /// Gets or sets the name of the party.
        /// </summary>
        /// <value>
        /// The name of the party.
        /// </value>
        private string PartyName { get; set; }
    }
}