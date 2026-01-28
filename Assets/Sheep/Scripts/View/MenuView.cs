using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sheep
{
	public class MenuView : BaseView
	{
		[SerializeField] RectTransform rectTransform;
		[SerializeField] Button takeback;


		[SerializeField] Button setting;
		[SerializeField] Button exit;




		public RectTransform RectTransform => rectTransform;


		#region Register

		public void RegisterTakeBackPressedEvent(UnityAction action)
		{
			takeback?.onClick.AddListener(action);
		}

		public void RegisterSetPressedEvent(UnityAction action)
		{
			setting?.onClick.AddListener(action);
		}

		public void RegisterExitEvent(UnityAction action)
		{
			exit?.onClick.AddListener(action);
		}

		#endregion


		#region UnRegister

		public void UnRegisterTakeBackPressedEvent(UnityAction action)
		{
			takeback?.onClick.RemoveListener(action);
		}

		public void UnRegisterSettingEvent(UnityAction action)
		{
			setting?.onClick.RemoveListener(action);
		}

		public void UnRegisterExitEvent(UnityAction action)
		{
			exit?.onClick.RemoveListener(action);
		}

		#endregion

	}
}


