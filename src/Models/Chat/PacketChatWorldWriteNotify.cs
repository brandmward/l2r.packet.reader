﻿using System.Collections.Generic;

namespace Kamael.Packets.Chat
{
    public class PacketChatWorldWriteNotify : IL2RPacket
    {
        public PacketChatWorldWriteNotify(IL2RPacket packet)
        {
            ChatList.Add(new PacketChat(packet));
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