using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sheep
{
    public class HomeView : BaseView
    {
        [SerializeField] Button begin;



		#region Register

		public void RegisterBeginPressedEvent(UnityAction action)
		{
			begin?.onClick.AddListener(action);
		}

		#endregion

		#region UnRegister

		public void UnRegisterBeginPressedEvent(UnityAction action)
		{
			begin?.onClick.RemoveListener(action);
		}

		#endregion

	}
}