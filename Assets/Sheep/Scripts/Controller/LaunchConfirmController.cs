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


