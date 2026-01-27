using QMVC;
using UnityEngine;

namespace Sheep
{

    public class GameOverController : MonoController
    {
        [SerializeField] GameOverView _gameOverView;

        AudioSystem _audioSystem;
        LevelModel _levelModel;


        public override void Init()
        {
            _audioSystem = this.GetSystem<AudioSystem>();
            _levelModel = this.GetModel<LevelModel>();


            _gameOverView.RegisterResurrectionPressed(OnResurrectionPressed);
            _gameOverView.RegisterCancelPressed(OnCancelPressed);

			this.RegisterEvent<GameOverEvent>(OnGameOver);
            gameObject.SetActive(false);
        }




        private void OnGameOver(GameOverEvent evt)
        {
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
				//¹Ø±Õlevel
				this.SendCommand(new LevelVisibleCommand(false));
				this.SendCommand(new HomeViewVisibleCommand(true));
				this.SendCommand(new MaskVisibleCommand(false));
				gameObject.SetActive(false);
			}, 1));
		}

	}
}


