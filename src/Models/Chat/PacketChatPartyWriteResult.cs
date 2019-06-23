using Kamael.Packets.ExtensionMethods;
using System.Collections.Generic;

namespace Kamael.Packets.Chat
{
    public class PacketChatPartyWriteResult : IL2RPacket
    {
        public PacketChatPartyWriteResult(IL2RPacket packet)
        {
            //packet.Skip(2);

            ushort numMessages = packet.ReadUInt16();

            for (int i = 0; i < numMessages; i++)
            {
                ChatList.Add(new PacketChat(packet));
            }
        }

        public byte[] Bytes { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the chat list.
        /// </summary>
        /// <value>
        /// The chat list.
        /// </value>
        private List<PacketChat> ChatList { get; set; }
    }
}