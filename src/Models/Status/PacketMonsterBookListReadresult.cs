﻿using System.IO;

namespace Kamael.Packets.Parser.Parsers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Kamael.Packets.IL2RPacket" />
    public class PacketMonsterBookListReadresult : IL2RPacket
    {
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
        /// Initializes a new instance of the <see cref="PacketMonsterBookListReadresult" /> class.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public PacketMonsterBookListReadresult(PacketReader packet)
        {
            using (StreamWriter fileStream = new StreamWriter(@"Output\PktMonsterBookListReadresult.csv", true))
            {
                packet.Skip(2);

                ushort TotalCodex = packet.ReadUInt16();
                fileStream.WriteLineAsync("Codex Entries: " + TotalCodex);
                fileStream.WriteLineAsync("\nCodexID,Unk1,Level,Cores");

                for (int j = 0; j < TotalCodex; j++)
                {
                    ushort CodexID = packet.ReadUInt16();
                    ushort Cores = packet.ReadUInt16();
                    ushort Level = packet.ReadUInt16();
                    ushort Unk1 = packet.ReadUInt16();
                    fileStream.WriteLineAsync(CodexID + "," + Cores + "," + Level + "," + Unk1);
                }

                ushort TotalCodexGroup = packet.ReadUInt16();
                fileStream.WriteLineAsync("Codex Pages: " + TotalCodexGroup);
                for (int j = 0; j < TotalCodexGroup; j++)
                {
                    ushort Unk1 = packet.ReadUInt16();
                    ushort CodexGroup = packet.ReadUInt16();
                    byte spacer = packet.ReadByte();
                    ushort Unk2 = packet.ReadUInt16();
                    ushort Unk3 = packet.ReadUInt16();
                }
            }
        }
    }
}