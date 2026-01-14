
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sheep
{
	public class MenuView : BaseView
	{
		[SerializeField] RectTransform rectTransform;
		[SerializeField] Button takeback;

		public RectTransform RectTransform => rectTransform;


		#region Register

		public void RegisterTakeBackPressedEvent(UnityAction action)
		{
			takeback?.onClick.AddListener(action);
		}

		#endregion


		#region UnRegister

		public void UnRegisterTakeBackPressedEvent(UnityAction action)
		{
			takeback?.onClick.RemoveListener(action);
		}

		#endregion

	}
}


