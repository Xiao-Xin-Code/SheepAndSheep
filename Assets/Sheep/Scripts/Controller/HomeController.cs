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
            this.RegisterEvent<HomeViewVisibleEvent>(HomeViewVisible);
            this.RegisterEvent<MaskVisibleEvent>(MaskVisible);
        }

        private void OnJoinPressed()
        {
            this.SendCommand<JoinCommand>();
        }

        private void OnMenuPressed()
        {
            this.SendCommand<UnFoldMenuCommand>();
        }


        private void HomeViewVisible(HomeViewVisibleEvent evt)
        {
            gameObject.SetActive(evt.visible);
        }

        private void MaskVisible(MaskVisibleEvent evt)
        {
            _view.MaskController(evt.visible);
        }

    }
}


