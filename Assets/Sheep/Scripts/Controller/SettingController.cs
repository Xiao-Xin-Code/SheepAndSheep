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
			_dataModel = this.GetModel<DataModel>();

			_settingView.RegisterMusicIsonChanged(OnMusicIsonChanged);
            _settingView.RegisterSfxIsonChanged(OnSfxIsonChanged);
            _settingView.RegisterShakeIsonChanged(OnShakeIsonChanged);
            _settingView.RegisterSkipLevelChanged(OnSkipLevelChanged);
			_settingView.RegisterClosePressed(OnClosePressed);

			this.RegisterEvent<SettingVisibleEvent>(SettingVisible);


			_settingView.SetMusicIsonWithoutNotify(_dataModel.MusicIsOn.Value);
			_settingView.SetSfxIsonWithoutNotify(_dataModel.SfxIsOn.Value);
			_settingView.SetShakeIsonWithoutNotify(_dataModel.ShakeIsOn.Value);
			_settingView.SetSkipIsonWithoutNotify(_dataModel.skipFirstLevel);

        }


        private void Start()
        {
			gameObject.SetActive(false);
		}



        private void OnMusicIsonChanged(bool isOn)
        {
            _dataModel.MusicIsOn.Value = isOn;
            if (isOn)
			{
				_audioSystem.PlayBGM("MainMusic");
			}
			else
			{
				_audioSystem.StopBGM();
			}
            _audioSystem.PlaySFX("Click");
        }

        private void OnSfxIsonChanged(bool isOn)
        {
            _dataModel.SfxIsOn.Value = isOn;
            if (isOn)
			{

			}
			else
			{
				_poolSystem.RecycleAllSFX();
			}
            _audioSystem.PlaySFX("Click");
        }

        private void OnShakeIsonChanged(bool isOn)
        {
           
        }

        private void OnSkipLevelChanged(bool isOn)
        {
            _dataModel.skipFirstLevel = isOn;
            _audioSystem.PlaySFX("Click");
        }


		private void OnClosePressed()
		{
            _audioSystem.PlaySFX("Click");
            gameObject.SetActive(false);
            this.SendCommand(new MaskVisibleCommand(false));
        }


		private void SettingVisible(SettingVisibleEvent evt)
		{
            if (evt.visible)
            {
                _settingView.SetMusicIsonWithoutNotify(_dataModel.MusicIsOn.Value);
                _settingView.SetSfxIsonWithoutNotify(_dataModel.SfxIsOn.Value);
                _settingView.SetShakeIsonWithoutNotify(_dataModel.ShakeIsOn.Value);
                _settingView.SetSkipIsonWithoutNotify(_dataModel.skipFirstLevel);
            }

			gameObject.SetActive(evt.visible);
            this.SendCommand(new MaskVisibleCommand(evt.visible));
        }
    }

}

