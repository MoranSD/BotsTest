using UnityEngine;
using UnityEngine.AI;
using Tools;
using Interfaces;

namespace BotSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Bot : MonoBehaviour
    {
        /*
         * если инвентарь не полный, идем к кубикам.
         * как подошли к кубикам начинаем процесс сборки.
         * как только инвентарь заполнился мы идем к сборщику кубов
         */
        public NavMeshAgent NavMeshAgent { get; private set; }
        public BotInventory Inventory { get; private set; }
        public StateMachine StateMachine { get; private set; }
        public IState CollectingBoxesState { get; private set; }
        public IState DropBoxesState { get; private set; }

        private const int inventoryCapacity = 10;
        private const int startBoxCountInInventory = 0;

        private void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Inventory = new BotInventory(inventoryCapacity, startBoxCountInInventory);

            CollectingBoxesState = new CollectingBoxesState(this);
            DropBoxesState = new DropBoxesState(this);
            StateMachine = new StateMachine(CollectingBoxesState);
        }
    }
}
