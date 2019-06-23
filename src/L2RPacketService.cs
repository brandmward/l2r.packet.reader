using Kamael.Packets.Factory;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;

namespace Kamael.Packets
{
    /// <summary>
    /// L2RPacket Service
    /// </summary>
    public partial class L2RPacketService
    {
        public ICaptureDevice Device { get; set; }

        // A few variable used throughout the program
        /// <summary>
        /// The encryption key
        /// </summary>
        public byte[] EncryptionKey = { 0xA7, 0x84, 0x20, 0xD0, 0xC9, 0x78, 0xB3, 0x9A };

        /// <summary>
        /// The filter
        /// </summary>
        public string filter = "port 12000 and len > 60";

        /// <summary>
        /// The read timeout milliseconds
        /// </summary>
        public int readTimeoutMilliseconds = 1000;

        /// <summary>
        /// The default device
        /// </summary>
        public int defaultDevice = 0;

        /// <summary>
        /// Parses the packet.
        /// </summary>
        /// <param name="packetReader">The packet reader.</param>
        /// <returns></returns>
        ///
        /// <summary>
        /// When true the background thread will terminate
        /// </summary>
        /// <param name="args">
        /// A <see cref="System.String"/>
        /// </param>
        private bool BackgroundThreadStop;

        /// <summary>
        /// Object that is used to prevent two threads from accessing
        /// PacketQueue at the same time
        /// </summary>
        /// <param name="args">
        /// A <see cref="System.String"/>
        /// </param>
        private readonly object QueueLock = new object();

        /// <summary>
        /// The queue that the callback thread puts packets in. Accessed by
        /// the background thread when QueueLock is held
        /// </summary>
        private List<RawCapture> PacketQueue = new List<RawCapture>();

        /// <summary>
        /// The last time PcapDevice.Statistics() was called on the active device.
        /// Allow periodic display of device statistics
        /// </summary>
        /// <param name="args">
        /// A <see cref="System.String"/>
        /// </param>
        private DateTime LastStatisticsOutput;

        /// <summary>
        /// Interval between PcapDevice.Statistics() output
        /// </summary>
        /// <param name="args">
        /// A <see cref="System.String"/>
        /// </param>
        ///

        /// <summary>
        /// The incoming buffer
        /// </summary>
        public System.Collections.Generic.List<byte> _incomingBuffer = new System.Collections.Generic.List<byte>();

        private readonly TimeSpan LastStatisticsInterval = new TimeSpan(0, 0, 2);
        private System.Threading.Thread backgroundThread;
        private PacketArrivalEventHandler arrivalEventHandler;
        private CaptureStoppedEventHandler captureStoppedEventHandler;
        private ICaptureDevice device;
        private Queue<IL2RPacket> packetQue;

        private int packetCount;
        private readonly BindingSource bs;
        private ICaptureStatistics captureStatistics;
        private bool statisticsUiNeedsUpdate = false;

        public event EventHandler<L2RPacketArrivalEventArgs> PacketArrivalEvent;
        public event EventHandler<L2RPacketErrorEventArgs> PacketError;
        public event EventHandler<L2RProcessQueueTickEventArgs> ProcessQueueTick;

        //public delegate IL2RPacket L2RPacketEventArrival2(byte[] payloadData);

        //Constructors
        public L2RPacketService()
        {
        }

        public L2RPacketService(ICaptureDevice Device)
        {
            device = Device;
            //StartCapture();
        }

        public L2RPacketService(int itemIndex)
        {
            device = CaptureDeviceList.Instance[itemIndex];
            //StartCapture();
        }

        public void StartCapture(ICaptureDevice Device)
        {
            device = Device;
            StartCapture();
        }

        public void StartCapture(int itemIndex)
        {
            device = CaptureDeviceList.Instance[itemIndex];
            StartCapture();
        }

        /// <summary>
        /// Start capturing packets.
        /// </summary>
        public void StartCapture()
        {
            packetCount = 0;

            if (device is null)
            {
                device = CaptureDeviceList.Instance[0];
            }

            packetQue = new Queue<IL2RPacket>();
            //bs = new BindingSource();
            //dataGridView.DataSource = bs;
            LastStatisticsOutput = DateTime.Now;


            // setup background capture
            arrivalEventHandler = new PacketArrivalEventHandler(Device_OnPacketArrival);
            device.OnPacketArrival += arrivalEventHandler;
            captureStoppedEventHandler = new CaptureStoppedEventHandler(Device_OnCaptureStopped);
            device.OnCaptureStopped += captureStoppedEventHandler;


            // start the background thread
            BackgroundThreadStop = false;
            backgroundThread = new System.Threading.Thread(BackgroundThread);
            backgroundThread.Start();

            device.Open();
            //filter to capture only packets from L2R that have data
            //string filter = "src port 12000 and len > 60";
            device.Filter = filter;
            // force an initial statistics update
            captureStatistics = device.Statistics;
            UpdateCaptureStatistics();


            // start the background capture
            device.StartCapture();
        }

        /// <summary>
        /// Stops capturing packets.
        /// </summary>
        public void StopCapture()
        {
            if (device != null)
            {
                device.StopCapture();
                device.Close();
                device.OnPacketArrival -= arrivalEventHandler;
                device.OnCaptureStopped -= captureStoppedEventHandler;
                device = null;

                // ask the background thread to shut down
                BackgroundThreadStop = true;

                // wait for the background thread to terminate
                backgroundThread.Join();

                //// switch the icon back to the play icon
                //startStopToolStripButton.Image = global::WinformsExample.Properties.Resources.play_icon_enabled;
                //startStopToolStripButton.ToolTipText = "Select device to capture from";
            }
        }

        private void Device_OnCaptureStopped(object sender, CaptureStoppedEventStatus status)
        {
            if (status != CaptureStoppedEventStatus.CompletedWithoutError)
            {
                //MessageBox.Show("Error stopping capture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            // print out periodic statistics about this device
            DateTime now = DateTime.Now; // cache 'DateTime.Now' for minor reduction in cpu overhead
            TimeSpan interval = now - LastStatisticsOutput;

            if (interval > LastStatisticsInterval)
            {
                captureStatistics = e.Device.Statistics;
                statisticsUiNeedsUpdate = true;
                LastStatisticsOutput = now;
            }

            // lock QueueLock to prevent multiple threads accessing PacketQueue at
            // the same time
            lock (QueueLock)
            {
                PacketQueue.Add(e.Packet);
            }
        }

        protected virtual void OnL2RPacketArrival(L2RPacketArrivalEventArgs e)
        {
            PacketArrivalEvent?.Invoke(this, e);
        }

        /// <summary>
        /// Decrypts the packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public void DecryptPacket(byte[] packet)
        {
            for (int i = 0; i < packet.Length; i++)
            {
                packet[i] = (byte)(packet[i] ^ EncryptionKey[i % EncryptionKey.Length]);
            }
        }


        /// <summary>
        /// Checks for queued packets. If any exist it locks the QueueLock, saves a
        /// reference of the current queue for itself, puts a new queue back into
        /// place into PacketQueue and unlocks QueueLock. This is a minimal amount of
        /// work done while the queue is locked.
        ///
        /// The background thread can then process queue that it saved without holding
        /// the queue lock.
        /// </summary>
        private void BackgroundThread()
        {
            while (!BackgroundThreadStop)
            {
                bool shouldSleep = true;

                lock (QueueLock)
                {
                    if (PacketQueue.Count != 0)
                    {
                        shouldSleep = false;
                    }
                }

                if (shouldSleep)
                {
                    System.Threading.Thread.Sleep(250);
                }
                else // should process the queue
                {
                    List<RawCapture> ourQueue;

                    lock (QueueLock)
                    {
                        // swap queues, giving the capture callback a new one
                        ourQueue = PacketQueue;
                        PacketQueue = new List<RawCapture>();
                    }
                    
                    ProcessQueueTick?.Invoke(this, new L2RProcessQueueTickEventArgs(ourQueue.Count));

                    foreach (RawCapture packet in ourQueue)
                    {
                        // Here is where we can process our packets freely without
                        // holding off packet capture.
                        //
                        // NOTE: If the incoming packet rate is greater than
                        //       the packet processing rate these queues will grow
                        //       to enormous sizes. Packets should be dropped in these
                        //       cases

                        try
                        {
                            PacketWrapper packetWrapper = new PacketWrapper(packetCount, packet);

                            Packet parsepacket = Packet.ParsePacket(packet.LinkLayerType, packet.Data);

                            TcpPacket tcpPacket = (TcpPacket)parsepacket.Extract(typeof(TcpPacket));
                            if (tcpPacket != null)
                            {

                                byte[] packetData = tcpPacket.PayloadData;

                                if (packetData != null)
                                {
                                    ProcessPackets(packetData);
                                }

                                packetCount++;
                            }

                            if (statisticsUiNeedsUpdate)
                            {
                                UpdateCaptureStatistics();
                                statisticsUiNeedsUpdate = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            PacketError?.Invoke(this, new L2RPacketErrorEventArgs(ex, packet));
                            throw;
                        }

                    }
                }
            }
        }

        private void ProcessPackets(byte[] payloadData)
        {
            try
            {
                if (payloadData == null || payloadData.Length < 2)
                {
                    return;
                }

                //get packetlength
                byte[] tmparray = new byte[2];
                tmparray[0] = payloadData[0];
                tmparray[1] = payloadData[1];
                ushort packetLength = BitConverter.ToUInt16(tmparray, 0);

                if (payloadData.Length >= packetLength)
                {
                    byte spacer = payloadData[2]; // skip 1 byte

                    byte[] packetData = new byte [payloadData.Length- 3];
                    //payloadData.CopyTo(packetData, 3);
                    Array.Copy(payloadData, 3, packetData, 0, packetLength - 3);
                    DecryptPacket(packetData);
                    byte[] packetBytes = packetData;

                    //Get packet id
                    L2RPacket packetReader = new L2RPacket(packetData);
                    ushort packetId = (ushort)(packetReader.ReadUInt16() - 1);

                    PacketFactory factory = new ConcretePacketFactory();
                    IL2RPacket l2rpckt = factory.GetPacket(packetId, packetReader);

                    //FIRE L2RPacketArrivalEvent
                    L2RPacketArrivalEventArgs args = new L2RPacketArrivalEventArgs
                    {
                        ID = packetId,
                        Packet = l2rpckt,
                        PacketBytes = packetBytes
                    };

                    OnL2RPacketArrival(args);

                    packetQue.Enqueue(l2rpckt);
                }
            }
            catch (Exception ex)
            {
                _incomingBuffer.Clear();

                PacketError?.Invoke(this, new L2RPacketErrorEventArgs(ex));
            }
        }

        private string UpdateCaptureStatistics()
        {
            string str = 
                $"Received packets: { captureStatistics.ReceivedPackets }, " + 
                $"Dropped packets: { captureStatistics.DroppedPackets }, " +
                $"Interface dropped packets: { captureStatistics.InterfaceDroppedPackets }";

            return str;
        }
    }
}