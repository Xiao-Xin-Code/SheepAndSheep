using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sheep
{

    public class LevelSettingView : BaseView
    {

        [SerializeField] Button proceed;
        [SerializeField] Button abandon;


		#region Register

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


