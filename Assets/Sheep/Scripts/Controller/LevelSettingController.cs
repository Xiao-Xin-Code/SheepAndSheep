using QMVC;
using UnityEngine;

namespace Sheep
{

    public class LevelSettingController : MonoController
    {
        [SerializeField] LevelSettingView _levelSettingView;

        AudioSystem _audioSystem;
        PoolSystem _poolSystem;


        public override void Init()
        {
            _audioSystem = this.GetSystem<AudioSystem>();

            this.RegisterEvent<LevelSetVisibleEvent>(LevelSetVisible);

            _levelSettingView.RegisterProceedPressed(OnProceedPressed);
            _levelSettingView.RegisterAbandonPressed(OnAbandonPressed);
            _levelSettingView.RegisterMusicIsonChanged(OnMusicIsonChanged);
            _levelSettingView.RegiterSfxIsonChanged(OnSfxIsonChanged);
            _levelSettingView.RegisterShakeIsonChanged(OnShakeIsonChanged);

            gameObject.SetActive(false);
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


        private void OnMusicIsonChanged(bool isOn)
        {
            if (isOn)
            {
                _audioSystem.LaunchBGM();
            }
            else
            {
                _audioSystem.StopBGM();
            }
        }

        private void OnSfxIsonChanged(bool isOn)
        {
            if (isOn)
            {

            }
            else
            {
                _poolSystem.RecycleAllSFX();
            }
        }

        private void OnShakeIsonChanged(bool isOn)
        {

        }
	}
}


