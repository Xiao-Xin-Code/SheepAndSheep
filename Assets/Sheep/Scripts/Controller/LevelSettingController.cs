using QMVC;
using UnityEngine;

namespace Sheep
{

    public class LevelSettingController : MonoController
    {
        [SerializeField] LevelSettingView _levelSettingView;

        AudioSystem _audioSystem;
        PoolSystem _poolSystem;
        LevelSystem _levelSystem;
        DataModel _dataModel;


        public override void Init()
        {
            _audioSystem = this.GetSystem<AudioSystem>();
            _poolSystem = this.GetSystem<PoolSystem>();
            _levelSystem = this.GetSystem<LevelSystem>();
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
            _audioSystem.PlaySFX("Click");
            gameObject.SetActive(false);
            this.SendCommand(new MaskVisibleCommand(false));
        }

        private void OnAbandonPressed()
        {
			_audioSystem.StopBGM();
			_audioSystem.PlaySFX("Click");
			this.SendCommand(new LaunchTransitionCommand(() =>
			{
				_poolSystem.RecycleAllBlock();//回收使用的Block
				_levelSystem.ClearBlocks();//清空关卡中的Block
                this.SendCommand<ClearContainerCommand>();

				//关闭level
				this.SendCommand(new LevelVisibleCommand(false));
				this.SendCommand(new HomeViewVisibleCommand(true));
				this.SendCommand(new MaskVisibleCommand(false));
				gameObject.SetActive(false);
			}, 1));
		}


        private void LevelSetVisible(LevelSetVisibleEvent evt)
        {
            this.SendCommand(new MaskVisibleCommand(evt.visible));
            gameObject.SetActive(evt.visible);
        }


        private void OnMusicIsonChanged(bool isOn)
        {
            _dataModel.MusicIsOn.Value = isOn;
            if (isOn)
            {
                _audioSystem.LaunchBGM();
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
	}
}


