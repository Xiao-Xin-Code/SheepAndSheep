using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sheep
{
    public class GameOverView : BaseView
    {

        [SerializeField] Button resurrection;
        [SerializeField] Button cancel;


		#region Register

        public void RegisterResurrectionPressed(UnityAction action)
        {
            resurrection?.onClick.AddListener(action);
        }

        public void RegisterCancelPressed(UnityAction action)
        {
            cancel?.onClick.AddListener(action);
        }

        #endregion

        #region UnRegister

        public void UnRegisterResurrectionPressed(UnityAction action)
        {
            resurrection?.onClick.RemoveListener(action);
        }

        public void UnRegisterCancelPressed(UnityAction action)
        {
            cancel?.onClick.RemoveListener(action);
        }

		#endregion

	}
}


