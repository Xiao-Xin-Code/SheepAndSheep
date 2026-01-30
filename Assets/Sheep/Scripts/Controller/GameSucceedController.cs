using QMVC;
using UnityEngine;

namespace Sheep
{
    public class GameSucceedController : MonoController
    {
        [SerializeField] GameSucceedView _gameSucceedView;

        AudioSystem _audioSystem;
        PoolSystem _poolSystem;
        LevelSystem _levelSystem;

        public override void Init()
        {
            _audioSystem = this.GetSystem<AudioSystem>();
            _poolSystem = this.GetSystem<PoolSystem>();
            _levelSystem = this.GetSystem<LevelSystem>();
            this.RegisterEvent<GameSucceedEvent>(OnGameSucceed);
            _gameSucceedView.RegisterBackGroupPressed(OnBackGroupPressed);
            
        }

        private void Start()
        {
			gameObject.SetActive(false);
		}


        private void OnGameSucceed(GameSucceedEvent evt)
        {
			this.SendCommand(new MaskVisibleCommand(true));
			//更新成就
			gameObject.SetActive(true);
        }

        private void OnBackGroupPressed()
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


