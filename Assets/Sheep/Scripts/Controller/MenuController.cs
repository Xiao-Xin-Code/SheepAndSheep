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
            _view.RegisterSetPressedEvent(OpenSetting);
            _view.RegisterExitEvent(Exit);

        }

        private void Start()
        {
			gameObject.SetActive(false);
		}


        private void UnFoldMenu(UnFoldMenuEvent evt)
        {
            this.SendCommand(new MaskVisibleCommand(true));
            gameObject.SetActive(true);
            DOTween.Kill(_view.RectTransform);
            _view.RectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.Linear);
		}

        private void TakeBackMenu()
        {
            _audioSystem.PlaySFX("Click");
            DOTween.Kill(_view.RectTransform);
            _view.RectTransform.DOAnchorPosX(-_view.RectTransform.rect.width, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                gameObject.SetActive(false);
                this.SendCommand(new MaskVisibleCommand(false));
            });
            
        }


        private void OpenSetting()
        {
            DOTween.Kill(_view.RectTransform);
            _view.RectTransform.DOAnchorPosX(-_view.RectTransform.rect.width, 0.1f).SetEase(Ease.Linear).OnComplete(() => {
                gameObject.SetActive(false);
                this.SendCommand(new SettingVisibleCommand(true));
            });
           
        }

        private void Exit()
        {
#if !UNITY_EDITOR
            Application.Quit();
#elif UNITY_EDITOR
            Debug.Log("ÍË³ö");
#endif

        }
	}
}


