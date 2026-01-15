using System.Collections;
using DG.Tweening;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class TransitionController : MonoController
    {
        [SerializeField] TransitionView _view;


        LevelModel _levelModel;

        public override void Init()
        {
            _levelModel = this.GetModel<LevelModel>();

            this.RegisterEvent<LaunchTransitionEvent>(LaunchTransition);

            gameObject.SetActive(false);
            
        }



        private void LaunchTransition(LaunchTransitionEvent evt)
        {
			_view.RectTransform.anchoredPosition = Vector2.zero;
			gameObject.SetActive(true);
			_view.RectTransform.DOAnchorPosX(-_view.RectTransform.rect.width, 1).SetEase(Ease.Linear).OnComplete(() =>
			{
                evt.Trigger();

				if (_levelModel.isLevelOver)
				{
					TransitionEnd();
				}
				else
				{
					StartCoroutine(WaitLevelLoadOver());
				}
			});
		}

        private void TransitionEnd()
        {
            _view.RectTransform.DOAnchorPosX(-2 * _view.RectTransform.rect.width, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
		}



        IEnumerator WaitLevelLoadOver()
        {
            while (true)
            {
                if (_levelModel.isLevelOver)
                {
					_view.RectTransform.DOAnchorPosX(-2 * _view.RectTransform.rect.width, 2);
                    break;
				}
				yield return 0;
            }
           
		}
    }
}


