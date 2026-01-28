using DG.Tweening;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class MenuController : MonoController
    {
        [SerializeField] MenuView _view;

        AudioSystem _audioSystem;

        public override void Init()
        {
            _audioSystem = this.GetSystem<AudioSystem>();

            this.RegisterEvent<UnFoldMenuEvent>(UnFoldMenu);
            _view.RegisterTakeBackPressedEvent(TakeBackMenu);
            _view.RegisterExitEvent(Exit);

            gameObject.SetActive(false);
        }


        private void UnFoldMenu(UnFoldMenuEvent evt)
        {
            gameObject.SetActive(true);
            DOTween.Kill(_view.RectTransform);
            _view.RectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.Linear);
		}

        private void TakeBackMenu()
        {
            _audioSystem.PlaySFX("Click");
            DOTween.Kill(_view.RectTransform);
            _view.RectTransform.DOAnchorPosX(-_view.RectTransform.rect.width, 0.5f).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));
        }


        private void OpenSetting()
        {

        }

        private void Exit()
        {
        #if !UNITY_EDITOR
            Application.Quit();
        #endif
		}
	}
}


