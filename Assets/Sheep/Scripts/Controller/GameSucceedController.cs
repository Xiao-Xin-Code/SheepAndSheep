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
            gameObject.SetActive(true);
        }

        private void OnBackGroupPressed()
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
    }
}


