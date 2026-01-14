using DG.Tweening;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class MenuController : MonoController
    {
        [SerializeField] MenuView _view;

        public override void Init()
        {
            this.RegisterEvent<UnFoldMenuEvent>(UnFoldMenu);
            _view.RegisterTakeBackPressedEvent(TakeBackMenu);
        }


        private void UnFoldMenu(UnFoldMenuEvent evt)
        {
            DOTween.Kill(_view.RectTransform);
            _view.RectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.Linear);
		}

        private void TakeBackMenu()
        {
            DOTween.Kill(_view.RectTransform);
            _view.RectTransform.DOAnchorPosX(-_view.RectTransform.rect.width, 0.5f).SetEase(Ease.Linear);
        }
    }
}


