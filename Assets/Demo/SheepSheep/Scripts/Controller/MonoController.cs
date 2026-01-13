using QMVC;
using UnityEngine;

namespace SheepSheep
{
    public abstract class MonoController : MonoBehaviour, IController
    {
        private void Awake()
        {
            Init();
        }

        protected abstract void Init();


        public IArchitecture GetArchitecture()
        {
            return SheepSheep.Interface;
        }
    }
}

