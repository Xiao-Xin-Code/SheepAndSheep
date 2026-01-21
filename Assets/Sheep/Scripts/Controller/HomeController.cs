using QMVC;
using UnityEngine;

namespace Sheep
{
    public class HomeController : MonoController
    {
        [SerializeField] HomeView _view;

        AudioSystem _audioSystem;

        public override void Init()
        {
            _audioSystem = this.GetSystem<AudioSystem>();

            _view.RegisterJoinPressedEvent(OnJoinPressed);
            _view.RegisterMenuPressedEvent(OnMenuPressed);
            this.RegisterEvent<HomeViewVisibleEvent>(HomeViewVisible);
            this.RegisterEvent<MaskVisibleEvent>(MaskVisible);
            gameObject.SetActive(false);
        }

        private void OnJoinPressed()
        {
			_view.MaskController(true);
			this.SendCommand<JoinCommand>();
        }

        private void OnMenuPressed()
        {
            _audioSystem.PlaySFX("Click");
            this.SendCommand<UnFoldMenuCommand>();
        }


        private void HomeViewVisible(HomeViewVisibleEvent evt)
        {
            gameObject.SetActive(evt.visible);
            if (evt.visible) _audioSystem.PlayBGM("MainMusic");

		}

        private void MaskVisible(MaskVisibleEvent evt)
        {
            _view.MaskController(evt.visible);
        }

    }
}


