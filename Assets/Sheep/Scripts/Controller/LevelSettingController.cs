using QMVC;
using UnityEngine;

namespace Sheep
{

    public class LevelSettingController : MonoController
    {
        [SerializeField] LevelSettingView _levelSettingView;

        AudioSystem _audioSystem;


        public override void Init()
        {
            _audioSystem = this.GetSystem<AudioSystem>();

            this.RegisterEvent<LevelSetVisibleEvent>(LevelSetVisible);

            _levelSettingView.RegisterProceedPressed(OnProceedPressed);
            _levelSettingView.RegisterAbandonPressed(OnAbandonPressed);

        }




        private void OnProceedPressed()
        {
            gameObject.SetActive(false);
        }

        private void OnAbandonPressed()
        {
			_audioSystem.StopBGM();
			_audioSystem.PlaySFX("Click");
			this.SendCommand(new LaunchTransitionCommand(() =>
			{
				//¹Ø±Õlevel
				this.SendCommand(new LevelVisibleCommand(false));
				this.SendCommand(new HomeViewVisibleCommand(true));
				this.SendCommand(new MaskVisibleCommand(false));
				gameObject.SetActive(false);
			}, 1));
		}



        private void LevelSetVisible(LevelSetVisibleEvent evt)
        {
            gameObject.SetActive(evt.visible);
        }
	}
}


