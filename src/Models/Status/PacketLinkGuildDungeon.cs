﻿using Kamael.Packets.ExtensionMethods;

namespace Kamael.Packets.Status
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Kamael.Packets.IL2RPacket" />
    public class PacketLinkGuildDungeon : IL2RPacket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PacketLinkGuildDungeon" /> class.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public PacketLinkGuildDungeon(IL2RPacket packet)
        {
            DungeonInfoID = packet.ReadUInt64();
            GuildUID = packet.ReadUInt32();
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
        /// Gets or sets the dungeon information identifier.
        /// </summary>
        /// <value>
        /// The dungeon information identifier.
        /// </value>
        private ulong DungeonInfoID { get; set; }

        /// <summary>
        /// Gets or sets the guild uid.
        /// </summary>
        /// <value>
        /// The guild uid.
        /// </value>
        private uint GuildUID { get; set; }
    }
}