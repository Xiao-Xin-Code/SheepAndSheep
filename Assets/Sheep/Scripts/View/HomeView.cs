using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sheep
{
    public class HomeView : BaseView
    {
        [SerializeField] Button join;
		[SerializeField] Button menu;

		[SerializeField] GameObject mask;

		#region Register

		public void RegisterJoinPressedEvent(UnityAction action)
		{
			join?.onClick.AddListener(action);
		}

		public void RegisterMenuPressedEvent(UnityAction action)
		{
			menu?.onClick.AddListener(action);
		}

		#endregion

		#region UnRegister

		public void UnRegisterJoinPressedEvent(UnityAction action)
		{
			join?.onClick.RemoveListener(action);
		}

		public void UnRegisterMenuPressedEvent(UnityAction action)
		{
			menu?.onClick.RemoveListener(action);
		}

		#endregion


		public void MaskController(bool isOn)
		{
			mask.gameObject.SetActive(isOn);
        }

	}
}