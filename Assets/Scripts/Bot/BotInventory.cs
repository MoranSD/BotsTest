using UnityEngine;

namespace BotSystem
{
    public class BotInventory
    {
        public bool IsFull => BoxCount == Capacity;
        public bool IsEmpty => BoxCount == 0;
        public int Capacity { get; private set; }
        public int BoxCount { get; private set; }

        public BotInventory(int capacity, int startBoxCount)
        {
            Capacity = capacity;

            if (startBoxCount > capacity)
                throw new System.Exception("Start box count cant be higher than capacity");

            BoxCount = startBoxCount;
        }

        public bool TryAddBox()
        {
            if (BoxCount == Capacity)
                return false;

            BoxCount++;
            return true;
        }
        public bool TryGetBox()
        {
            if (BoxCount <= 0)
                return false;

            BoxCount--;
            return true;
        }
    }
}
