using QMVC;
using UnityEngine;

namespace Sheep
{
    public abstract class MonoController : MonoBehaviour, IController
    {
        private void Awake()
        {
            Init();
		}

        public abstract void Init();

        public IArchitecture GetArchitecture()
        {
            return Sheep.Interface;
        }
    }
}
