using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sheep
{
    public class LevelView : BaseView
    {

        [SerializeField] Button setting;
        [SerializeField] Text dateText;
        
        public void SetDate(string date)
        {
            dateText.text = date;
        }


        #region Register

        public void RegisterSetPressed(UnityAction action)
        {
            setting?.onClick.AddListener(action);
        }

        #endregion


        #region UnRegister

        public void UnRegisterSetPressed(UnityAction action)
        {
            setting?.onClick.RemoveListener(action);
        }

        #endregion
    }

}

