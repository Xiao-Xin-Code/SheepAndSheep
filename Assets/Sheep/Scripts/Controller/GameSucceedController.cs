using QMVC;
using UnityEngine;

namespace Sheep
{
    public class GameSucceedController : MonoController
    {
        [SerializeField] GameSucceedView _gameSucceedView;

        AudioSystem _audioSystem;

        public override void Init()
        {
            _audioSystem = this.GetSystem<AudioSystem>();
            this.RegisterEvent<GameSucceedEvent>(OnGameSucceed);
            _gameSucceedView.RegisterBackGroupPressed(OnBackGroupPressed);
            gameObject.SetActive(false);
        }


        private void OnGameSucceed(GameSucceedEvent evt)
        {
            //更新成就
            gameObject.SetActive(true);
        }

        private void OnBackGroupPressed()
        {
            _audioSystem.StopBGM();
            _audioSystem.PlaySFX("Click");
            this.SendCommand(new LaunchTransitionCommand(() =>
            {
                //关闭level
                this.SendCommand(new LevelVisibleCommand(false));
                this.SendCommand(new HomeViewVisibleCommand(true));
                this.SendCommand(new MaskVisibleCommand(false));
                gameObject.SetActive(false);
            }, 1));
        }
    }
}


