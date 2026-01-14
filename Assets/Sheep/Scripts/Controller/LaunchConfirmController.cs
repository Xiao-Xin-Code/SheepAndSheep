using QMVC;
using UnityEngine;

namespace Sheep
{
    public class LaunchConfirmController : MonoController
    {
        [SerializeField] LaunchConfirmView _view;


        public override void Init()
        {
            _view.RegisterLaunchPressedEvent(OnLaunchPressed);
            _view.RegisterClosePressedEvent(OnClosePressed);
            this.RegisterEvent<JoinEvent>(JoinCallBack);

            gameObject.SetActive(false);
        }


        private void JoinCallBack(JoinEvent evt)
        {
            gameObject.SetActive(true);
        }


        private void OnLaunchPressed()
        {
            
            gameObject.SetActive(false);
        }

        private void OnClosePressed()
        {
            gameObject.SetActive(false);
        }

    }
}


