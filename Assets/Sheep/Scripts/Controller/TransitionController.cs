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

            gameObject.SetActive(false);
            
        }


        private void StageInit()
        {
            _view.RectTransform.anchoredPosition = Vector2.zero;
        }

        private void StageBegin()
        {
            _view.RectTransform.DOAnchorPosX(0, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (_levelModel.isLevelOver)
                {
                    StageEnd();
                }
                else
                {
                    StartCoroutine(WaitLevelLoadOver());
				}


            });
        }

        private void StageEnd()
        {
			_view.RectTransform.DOAnchorPosX(0, 1).SetEase(Ease.Linear).OnComplete(() =>
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


