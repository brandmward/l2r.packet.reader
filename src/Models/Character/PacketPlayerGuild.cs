﻿using Kamael.Packets.ExtensionMethods;

namespace Kamael.Packets.Character
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Kamael.Packets.IL2RPacket" />
    public class PacketPlayerGuild : IL2RPacket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PacketPlayerGuild" /> class.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public PacketPlayerGuild(IL2RPacket packet)
        {
            GuildID = packet.ReadUInt64();
            GuildMembersGrade = packet.ReadByte();
            GuildName = packet.ReadString();
            EmblemSymbolInfoID = packet.ReadUInt32();
            EmblemBackgroundInfoID = packet.ReadUInt32();
            OccupyTitleInfoID = packet.ReadUInt32();
            CastleType = packet.ReadByte();
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
        /// Gets or sets the type of the castle.
        /// </summary>
        /// <value>
        /// The type of the castle.
        /// </value>
        public byte CastleType { get; set; }

        /// <summary>
        /// Gets or sets the emblem background information identifier.
        /// </summary>
        /// <value>
        /// The emblem background information identifier.
        /// </value>
        public uint EmblemBackgroundInfoID { get; set; }

        /// <summary>
        /// Gets or sets the emblem symbol information identifier.
        /// </summary>
        /// <value>
        /// The emblem symbol information identifier.
        /// </value>
        public uint EmblemSymbolInfoID { get; set; }

        /// <summary>
        /// Gets or sets the guild identifier.
        /// </summary>
        /// <value>
        /// The guild identifier.
        /// </value>
        public ulong GuildID { get; set; }

        /// <summary>
        /// Gets or sets the guild members grade.
        /// </summary>
        /// <value>
        /// The guild members grade.
        /// </value>
        public byte GuildMembersGrade { get; set; }

        /// <summary>
        /// Gets or sets the name of the guild.
        /// </summary>
        /// <value>
        /// The name of the guild.
        /// </value>
        public string GuildName { get; set; }

        /// <summary>
        /// Gets or sets the occupy title information identifier.
        /// </summary>
        /// <value>
        /// The occupy title information identifier.
        /// </value>
        public uint OccupyTitleInfoID { get; set; }
    }
}