using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using Cysharp.Threading.Tasks;

namespace BotSystem
{
    public class DropBoxesState : IState, IPeaceOfQueue
    {
        private Bot _controller;
        public DropBoxesState(Bot controller)
        {
            if (controller == null)
                throw new System.Exception("Controller cant be null");

            _controller = controller;
        }

        public int Id { get; set; }

        private const float dropBoxTime = 0.5f;

        public void Enter()
        {
            if (_controller.Inventory.IsFull)
            {
                _controller.NavMeshAgent.SetDestination(BoxCollectorQueue.Instance.GetLastQueuePosition());
                BoxCollectorQueue.Instance.EnterQueue(this);
            }
            else
            {
                _controller.StateMachine.ChangeState(_controller.CollectingBoxesState);
            }
        }
        public async void OnFirstInQueue(System.Action CallBack)
        {
            await UniTask.WaitUntil(() => 
            {
                if (_controller == null)
                    return true;

                return _controller.NavMeshAgent.remainingDistance <= _controller.NavMeshAgent.stoppingDistance;
            });

            if (_controller == null)
                return;

            while (_controller.Inventory.IsEmpty == false)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(dropBoxTime));
                _controller.Inventory.TryGetBox();
            }

            _controller.StateMachine.ChangeState(_controller.CollectingBoxesState);
            CallBack?.Invoke();
        }

        public void OnQueueMove(Vector3 queuePosition)
        {
            _controller.NavMeshAgent.SetDestination(queuePosition);
        }
    }
}
