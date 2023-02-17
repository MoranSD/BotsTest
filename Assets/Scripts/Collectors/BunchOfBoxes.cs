using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

namespace Collectors
{
    public class BunchOfBoxes : MonoBehaviour
    {
        [SerializeField] private int _getBoxTime;
        public async UniTask GetBox()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_getBoxTime));
            return;
        }
    }
}
