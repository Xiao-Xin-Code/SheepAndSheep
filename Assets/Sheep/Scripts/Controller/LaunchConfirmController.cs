using System;
using System.Data;
using Frame;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class LaunchConfirmController : MonoController
    {
        [SerializeField] LaunchConfirmView _view;

        LevelSystem _levelSystem;
        LevelModel _levelModel;
        PoolSystem _poolSystem;
        AudioSystem _audioSystem;
        DataModel _dataModel;

        public override void Init()
        {
            _poolSystem = this.GetSystem<PoolSystem>();
            _levelSystem = this.GetSystem<LevelSystem>();
            _levelModel = this.GetModel<LevelModel>();
            _audioSystem = this.GetSystem<AudioSystem>();
            _dataModel = this.GetModel<DataModel>();

            _view.RegisterLaunchPressedEvent(OnLaunchPressed);
            _view.RegisterClosePressedEvent(OnClosePressed);
            this.RegisterEvent<JoinEvent>(JoinCallBack);

			
		}

        private void Start()
        {
			gameObject.SetActive(false);
		}


        private void JoinCallBack(JoinEvent evt)
        {
			DateTime now = DateTime.Now;
			_view.SetTime(now.ToString("- MM月dd日 -"));
            //初始设置关卡难度
            _levelModel.levelup = _dataModel.skipFirstLevel;
			gameObject.SetActive(true);
        }


        private void OnLaunchPressed()
        {
            _audioSystem.StopBGM();
            this.SendCommand(new LaunchTransitionCommand(Hide, 1));
			_poolSystem.RecycleAllBlock();
			_levelSystem.ClearBlocks();
            this.SendCommand<ClearContainerCommand>();
			this.SendCommand<LaunchLevelCommand>();
        }

        private void OnClosePressed()
        {
            gameObject.SetActive(false);
            this.SendCommand(new MaskVisibleCommand(false));
        }


        private void Hide()
        {
            this.SendCommand(new HomeViewVisibleCommand(false));
            this.SendCommand(new LevelVisibleCommand(true));
            this.SendCommand(new MaskVisibleCommand(false));
            gameObject.SetActive(false);
            _audioSystem.PlayBGM("BgMusic");
        }

    }
}


