using QMVC;
using UnityEngine;

namespace Sheep
{
    public class SettingController : MonoController
    {
        [SerializeField] SettingView _settingView;

		AudioSystem _audioSystem;
		PoolSystem _poolSystem;
		DataModel _dataModel;


        public override void Init()
        {
			_audioSystem = this.GetSystem<AudioSystem>();
			_poolSystem = this.GetSystem<PoolSystem>();

			_settingView.RegisterMusicIsonChanged(OnMusicIsonChanged);
            _settingView.RegisterSfxIsonChanged(OnSfxIsonChanged);
            _settingView.RegisterShakeIsonChanged(OnShakeIsonChanged);
            _settingView.RegisterSkipLevelChanged(OnSkipLevelChanged);

			this.RegisterEvent<SettingVisibleEvent>(SettingVisible);

            gameObject.SetActive(false);
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

        private void OnSkipLevelChanged(bool isOn)
        {

        }


		private void SettingVisible(SettingVisibleEvent evt)
		{
			gameObject.SetActive(evt.visible);
		}
    }

}

