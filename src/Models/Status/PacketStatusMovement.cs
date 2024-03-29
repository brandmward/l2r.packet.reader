﻿using System;

namespace Kamael.Packets
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Kamael.Packets.IL2RPacket" />
    /// <seealso cref="Kamael.Models.IL2RPacket" />
    [Serializable]
    public class PacketStatusMovement : IL2RPacket
    {
        /// <summary>
        /// The packet identifier
        /// </summary>
        public readonly ushort packetId = 202;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusMovement" /> class.
        /// </summary>
        /// <param name="Packet">The packet.</param>
        public PacketStatusMovement(IL2RPacket Packet)
        {
            packet = (L2RPacket)Packet;
            playerId = packet.ReadUInt64();
            playerMoveType = packet.ReadUInt16();
            playerDestXpos = packet.ReadSingle();
            playerDestYpos = packet.ReadSingle();
        }

        /// <summary>
        /// Gets or sets the bytes.
        /// </summary>
        /// <value>
        /// The bytes.
        /// </value>
        public byte[] Bytes { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the packet.
        /// </summary>
        /// <value>
        /// The packet.
        /// </value>
        public L2RPacket packet { get; set; }

        /// <summary>
        /// Gets or sets the player dest xpos.
        /// </summary>
        /// <value>
        /// The player dest xpos.
        /// </value>
        public float playerDestXpos { get; set; }

        /// <summary>
        /// Gets or sets the player dest ypos.
        /// </summary>
        /// <value>
        /// The player dest ypos.
        /// </value>
        public float playerDestYpos { get; set; }

        /// <summary>
        /// Gets or sets the player identifier.
        /// </summary>
        /// <value>
        /// The player identifier.
        /// </value>
        public ulong playerId { get; set; }

        /// <summary>
        /// Gets or sets the type of the player move.
        /// </summary>
        /// <value>
        /// The type of the player move.
        /// </value>
        public ushort playerMoveType { get; set; }
    }
}