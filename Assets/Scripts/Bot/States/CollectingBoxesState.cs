using UnityEngine.AI;
using Interfaces;
using Collectors;
using Cysharp.Threading.Tasks;

namespace BotSystem
{
    public class CollectingBoxesState : IState
    {
        private Bot _controller;
        private BunchOfBoxes _bunch;
        public CollectingBoxesState(Bot controller)
        {
            if (controller == null)
                throw new System.Exception("Controller cant be null");

            _controller = controller;
        }
        public void Enter()
        {
            if (_controller.Inventory.IsFull)
            {
                _controller.StateMachine.ChangeState(_controller.DropBoxesState);
            }
            else
            {
                _bunch = BoxStorage.Instance.GetBunchOfBoxes();
                _controller.NavMeshAgent.SetDestination(_bunch.transform.position);
                GetBoxesProcess();
            }
        }
        private async void GetBoxesProcess()
        {
            await UniTask.WaitUntil(() =>
            {
                if (_controller == null)
                    return true;

                return _controller.NavMeshAgent.remainingDistance <= _controller.NavMeshAgent.stoppingDistance;
            });

            if (_controller == null || _bunch == null)
                return;

            while(_controller.Inventory.IsFull == false)
            {
                await _bunch.GetBox();
                _controller.Inventory.TryAddBox();
            }

            _controller.StateMachine.ChangeState(_controller.DropBoxesState);
        }
    }
}
