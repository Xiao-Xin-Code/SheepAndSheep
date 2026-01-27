using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sheep
{

    public class LevelSettingView : BaseView
    {

        [SerializeField] Toggle musicIson;
        [SerializeField] Toggle sfxIson;
        [SerializeField] Toggle shakeIson;

        [SerializeField] Button proceed;
        [SerializeField] Button abandon;


		#region Register

        public void RegisterMusicIsonChanged(UnityAction<bool> action)
        {
            musicIson?.onValueChanged.AddListener(action);
        }

        public void RegiterSfxIsonChanged(UnityAction<bool> action)
        {
            sfxIson?.onValueChanged.AddListener(action);
        }

        public void RegisterShakeIsonChanged(UnityAction<bool> action)
        {
            shakeIson?.onValueChanged.AddListener(action);
        }


        public void RegisterProceedPressed(UnityAction action)
        {
            proceed?.onClick.AddListener(action);
        }

        public void RegisterAbandonPressed(UnityAction action)
        {
            abandon?.onClick.AddListener(action);
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

        public void UnRegisterProceedPressed(UnityAction action)
        {
            proceed?.onClick.RemoveListener(action);
        }

        public void UnRegisterAbandonPressed(UnityAction action)
        {
            abandon?.onClick.RemoveListener(action);
        }

        #endregion


    }

}


