using Kamael.Packets.Chat;
using Kamael.Packets.Clan;
using System;
using static Kamael.Packets.L2RPacketService;

namespace Kamael.Packets
{
    internal class Debugger
    {
        public static L2RPacketService PacketService { get; private set; }

        private static void Main(string[] args)
        {
            Globals.Args = args;
            Globals.DeviceInterface = 0;

            PacketService = new L2RPacketService(Globals.DeviceInterface);

            PacketService.ProcessQueueTick += OnL2RProcessQueueTick;
            PacketService.PacketArrivalEvent += OnL2RPacketArrival;
            PacketService.PacketError += OnL2RPacketError;

            PacketService.StartCapture();

            int counter = 0;

            while (true)
            {
                counter++;
            }
        }

        /// <summary>
        /// Subcribe to the tick event of the process queue if you'd like to tap into the general
        /// heartbeat of the packet service.
        /// </summary>
        private static void OnL2RProcessQueueTick(object sender, L2RProcessQueueTickEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Processing Queue ({ e.QueueCount } queued)");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Subscribe to the error pipeline of the packet service.
        /// </summary>
        private static void OnL2RPacketError(object sender, L2RPacketErrorEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(e.Exception.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Subcribe to the l2r packet arrival event (a packet has been recognized as an l2r packet).
        /// </summary>
        private static void OnL2RPacketArrival(object sender, L2RPacketArrivalEventArgs e)
        {
            var test = e.Packet;

            if (test != null)
            {
                //Console.WriteLine($"Packet Received: { e.Packet.GetType().FullName }");

                // just a couple of packet types that we may be interested in monitoring...

                if (e.Packet is PacketClanMemberKillNotify)
                {
                    var p = e.Packet as PacketClanMemberKillNotify;

                    Console.WriteLine($"Received Packet: PacketClanMemberKillNotify");
                    Console.WriteLine($"\t{ p.PlayerName } ({ p.ClanName }) murdered { p.Player2Name } ({ p.Clan2Name })");
                }

                else if (e.Packet is PacketChatGuildListReadResult)
                {
                    var p = e.Packet as PacketChatGuildListReadResult;

                    Console.WriteLine($"Received Packet: PacketChatGuildListReadResult");
                    Console.WriteLine($"\t[{ p.MessageTime }] { p.PlayerName }: { p.Message }");
                }
            }
        }
    }
}

