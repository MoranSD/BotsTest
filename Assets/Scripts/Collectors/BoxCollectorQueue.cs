using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Tools;

public class BoxCollectorQueue : Singleton<BoxCollectorQueue>
{
    [SerializeField] private Transform _queueStart;
    [SerializeField] private float _distanceBetweenPeaces;

    public IPeaceOfQueue FirstInQueue { get; private set; }

    private List<IPeaceOfQueue> _queue;
    private void Start()
    {
        _queue = new List<IPeaceOfQueue>();
    }

    public void EnterQueue(IPeaceOfQueue peace)
    {
        _queue.Add(peace);
        peace.Id = _queue.IndexOf(peace);

        if (_queue.Count == 1)
        {
            FirstInQueue = peace;
            peace.OnFirstInQueue(OnQueueMove);
        }
    }
    public Vector3 GetLastQueuePosition()
    {
        return GetQueuePosition(_queue.Count);
    }
    private void OnQueueMove()
    {
        _queue.RemoveAt(0);

        if (_queue.Count == 0) return;

        foreach (var item in _queue)
        {
            item.Id = _queue.IndexOf(item);
            item.OnQueueMove(GetQueuePosition(item.Id));
        }

        FirstInQueue = _queue[0];
        _queue[0].OnFirstInQueue(OnQueueMove);
    }
    private Vector3 GetQueuePosition(int queueId)
    {
        return _queueStart.position + (_queueStart.forward * _distanceBetweenPeaces * queueId);
    }
}
