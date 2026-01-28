using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sheep
{
    public class GameSucceedView : BaseView
    {
        [SerializeField] Button backGroup;


        #region Register

        public void RegisterBackGroupPressed(UnityAction action)
        {
            backGroup.onClick.AddListener(action);
        }

        #endregion

        #region UnRegister

        public void UnRegisterBackGroupPressed(UnityAction action)
        {
            backGroup.onClick.RemoveListener(action);
        }

        #endregion
    }

}

