using Kamael.Packets.ExtensionMethods;
using System.Collections.Generic;

namespace Kamael.Packets.Chat
{
    /// <summary>
    ///
    /// </summary>
    public class PacketChatGuildWriteResult : IL2RPacket
    {
        private List<PacketChat> _chatList;

        public byte[] Bytes { get; set; }
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the chat list.
        /// </summary>
        /// <value>
        /// The chat list.
        /// </value>
        private List<PacketChat> ChatList
        {
            get
            {
                return _chatList ?? new List<PacketChat>();
            }
            set
            {
                _chatList = value;
            }
        }

        /// <summary>
        /// Gets or sets the last chat identifier.
        /// </summary>
        /// <value>
        /// The last chat identifier.
        /// </value>
        private ulong LastChatID { get; set; }

        /// <summary>
        /// Gets or sets the number messages.
        /// </summary>
        /// <value>
        /// The number messages.
        /// </value>
        private ushort numMessages { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketChatGuildWriteResult"/> class.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public PacketChatGuildWriteResult(IL2RPacket packet)
        {
            packet.Skip(2);

            LastChatID = packet.ReadUInt64();
            numMessages = packet.ReadUInt16();

            for (int i = 0; i < numMessages; i++)
            {
                ChatList.Add(new PacketChat(packet));
            }
        }
    }
}