using UnityEngine;

namespace Interfaces
{
    public interface IPeaceOfQueue
    {
        public int Id { get; set; }
        public void OnFirstInQueue(System.Action CallBack);
        public void OnQueueMove(Vector3 queuePosition);
    }
}
