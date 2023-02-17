using UnityEngine;
using Tools;

namespace Collectors
{
    public class BoxStorage : Singleton<BoxStorage>
    {
        [SerializeField] BunchOfBoxes[] _bunches;

        public BunchOfBoxes GetBunchOfBoxes()
        {
            int randomBunchId = Random.Range(0, _bunches.Length);
            return _bunches[randomBunchId];
        }
    }
}
