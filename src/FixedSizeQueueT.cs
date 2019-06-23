using System.Collections.Concurrent;

namespace Kamael.Packets
{
    /// <summary>
    /// Fixed size queue for preventing duplicate packet from being processed.
    /// </summary>
    public class FixedSizeQueue<T> : ConcurrentQueue<T>
    {
        private readonly object _syncObject = new object();

        public int Size { get; private set; }

        public FixedSizeQueue(int size)
        {
            Size = size;
        }

        public new void Enqueue(T obj)
        {
            base.Enqueue(obj);

            lock(_syncObject)
            {
                while (Count > Size)
                {
                    TryDequeue(out T outObj);
                }
            }
        }
    }
}
