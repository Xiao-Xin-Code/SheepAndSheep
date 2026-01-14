using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sheep
{

	public class LaunchConfirmView : BaseView
	{
		[SerializeField] Button launch;
		[SerializeField] Button close;



		#region Register

		public void RegisterLaunchPressedEvent(UnityAction action)
		{
			launch?.onClick.AddListener(action);
		}

		public void RegisterClosePressedEvent(UnityAction action)
		{
			close.onClick.AddListener(action);
		}

		#endregion


		#region UnRegister

		public void UnRegisterLaunchPressedEvent(UnityAction action)
		{
			launch?.onClick.RemoveListener(action);
		}

		public void UnRegisterClosePressedEvent(UnityAction action)
		{
			close?.onClick.RemoveListener(action);
		}

		#endregion


	}
}


