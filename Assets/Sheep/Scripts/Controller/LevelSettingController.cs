using QMVC;
using UnityEngine;

namespace Sheep
{

    public class LevelSettingController : MonoController
    {
        [SerializeField] LevelSettingView _levelSettingView;

        AudioSystem _audioSystem;
        PoolSystem _poolSystem;
        DataModel _dataModel;


        public override void Init()
        {
            _audioSystem = this.GetSystem<AudioSystem>();
            _poolSystem = this.GetSystem<PoolSystem>();
            _dataModel = this.GetModel<DataModel>();

            this.RegisterEvent<LevelSetVisibleEvent>(LevelSetVisible);

            _levelSettingView.RegisterClosePressed(OnClosePressed);
            _levelSettingView.RegisterAbandonPressed(OnAbandonPressed);
            _levelSettingView.RegisterMusicIsonChanged(OnMusicIsonChanged);
            _levelSettingView.RegiterSfxIsonChanged(OnSfxIsonChanged);
            _levelSettingView.RegisterShakeIsonChanged(OnShakeIsonChanged);

            gameObject.SetActive(false);
        }




        private void OnClosePressed()
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

            _dataModel.MusicIsOn.Value = isOn;
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

            _dataModel.SfxIsOn.Value = isOn;
        }

        private void OnShakeIsonChanged(bool isOn)
        {

        }
	}
}


