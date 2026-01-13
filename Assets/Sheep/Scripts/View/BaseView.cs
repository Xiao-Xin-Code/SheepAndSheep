using QMVC;
using UnityEngine;

namespace Sheep
{
	public class BaseView : MonoBehaviour,IView
	{
        public IArchitecture GetArchitecture()
        {
            return Sheep.Interface;
        }
	}
}


