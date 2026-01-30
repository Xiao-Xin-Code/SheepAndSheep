using QMVC;
using UnityEngine;

namespace Sheep
{

    public class GameOverController : MonoController
    {
        [SerializeField] GameOverView _gameOverView;

        AudioSystem _audioSystem;
        PoolSystem _poolSystem;
        LevelSystem _levelSystem;
        LevelModel _levelModel;


        public override void Init()
        {
            _audioSystem = this.GetSystem<AudioSystem>();
			_poolSystem = this.GetSystem<PoolSystem>();
            _levelSystem = this.GetSystem<LevelSystem>();
			_levelModel = this.GetModel<LevelModel>();
            


            _gameOverView.RegisterResurrectionPressed(OnResurrectionPressed);
            _gameOverView.RegisterCancelPressed(OnCancelPressed);

			this.RegisterEvent<GameOverEvent>(OnGameOver);
            
        }

        private void Start()
        {
			gameObject.SetActive(false);
		}




        private void OnGameOver(GameOverEvent evt)
        {
			this.SendCommand(new MaskVisibleCommand(true));
			_gameOverView.SetResurrection(evt.canResurrection);
            gameObject.SetActive(true);
        }


        private void OnResurrectionPressed()
        {
            _levelModel.levelState.Value = LevelState.Runtime;
            this.SendCommand<LevelResurrectionCommand>();
			this.SendCommand(new MaskVisibleCommand(false));
			gameObject.SetActive(false);
        }


        private void OnCancelPressed()
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

	}
}


