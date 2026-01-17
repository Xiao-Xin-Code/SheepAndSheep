using QMVC;
using UnityEngine;

namespace Sheep
{
    public class HomeController : MonoController
    {
        [SerializeField] HomeView _view;

        public override void Init()
        {
            _view.RegisterJoinPressedEvent(OnJoinPressed);
            _view.RegisterMenuPressedEvent(OnMenuPressed);
        }

        private void OnJoinPressed()
        {
            this.SendCommand<JoinCommand>();
        }

        private void OnMenuPressed()
        {
            this.SendCommand<UnFoldMenuCommand>();
        }


        private void ActiveHome(bool isOn)
        {
            gameObject.SetActive(isOn);
        }

    }
}


