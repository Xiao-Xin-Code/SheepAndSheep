using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sheep
{
	public class SettingView : BaseView
	{
		[SerializeField] Toggle musicIson;
		[SerializeField] Toggle sfxIson;
		[SerializeField] Toggle shakeIson;
		[SerializeField] Toggle skipFirstLevel;

		[SerializeField] Button close;




		#region Register

		public void RegisterMusicIsonChanged(UnityAction<bool> action)
		{
			musicIson?.onValueChanged.AddListener(action);
		}

		public void RegisterSfxIsonChanged(UnityAction<bool> action)
		{
			sfxIson?.onValueChanged.AddListener(action);
		}

		public void RegisterShakeIsonChanged(UnityAction<bool> action)
		{
			shakeIson?.onValueChanged.AddListener(action);
		}

		public void RegisterSkipLevelChanged(UnityAction<bool> action)
		{
			skipFirstLevel?.onValueChanged.AddListener(action);
		}


		public void RegisterClosePressed(UnityAction action)
		{
			close?.onClick.AddListener(action); 
		}

		#endregion


		#region UnRegister

		public void UnRegisterMusicIsonChanged(UnityAction<bool> action)
		{
			musicIson?.onValueChanged.RemoveListener(action);
		}

		public void UnRegisterSfxIsonChanged(UnityAction<bool> action)
		{
			sfxIson?.onValueChanged.RemoveListener(action);
		}

		public void UnRegisterShakeIsonChanged(UnityAction<bool> action)
		{
			shakeIson?.onValueChanged.RemoveListener(action);
		}

		public void UnRegisterSkipLevelChanged(UnityAction<bool> action)
		{
			skipFirstLevel?.onValueChanged.RemoveListener(action);
		}

		#endregion


		public void SetMusicIsonWithoutNotify(bool isOn)
		{
			musicIson?.SetIsOnWithoutNotify(isOn);
		}

        public void SetSfxIsonWithoutNotify(bool isOn)
        {
            sfxIson?.SetIsOnWithoutNotify(isOn);
        }

        public void SetShakeIsonWithoutNotify(bool isOn)
        {
            shakeIson?.SetIsOnWithoutNotify(isOn);
        }

        public void SetSkipIsonWithoutNotify(bool isOn)
        {
            skipFirstLevel?.SetIsOnWithoutNotify(isOn);
        }

    }

}


