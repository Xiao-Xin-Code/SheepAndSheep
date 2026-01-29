using System.Collections;
using DG.Tweening;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class TransitionController : MonoController
    {
        [SerializeField] TransitionView _view;

        AudioSystem _audioSystem;
        LevelModel _levelModel;

        public override void Init()
        {
            _levelModel = this.GetModel<LevelModel>();
            _audioSystem = this.GetSystem<AudioSystem>();
            this.RegisterEvent<LaunchTransitionEvent>(LaunchTransition);

            gameObject.SetActive(false);
            
        }



        private void LaunchTransition(LaunchTransitionEvent evt)
        {
			_view.RectTransform.anchoredPosition = Vector2.zero;
			_view.SheepGroup.SetActive(evt.state == 1);
			_view.LevelUp.SetActive(evt.state == 2);
			gameObject.SetActive(true);
            _audioSystem.PlaySFX("Click");

            //ÒÆ¶¯¸²¸Ç
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_view.RectTransform.DOAnchorPosX(-_view.RectTransform.rect.width, 1).SetEase(Ease.Linear));
            if(evt.state == 2) sequence.AppendInterval(0.5f);
            sequence.OnComplete(() =>
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
            sequence.Play();
            //_view.RectTransform.DOAnchorPosX(-_view.RectTransform.rect.width, 1).SetEase(Ease.Linear).OnComplete(() =>
            //{
            //    evt.Trigger();
            //    if (_levelModel.isLevelOver)
            //    {
            //        TransitionEnd();
            //    }
            //    else
            //    {
            //        StartCoroutine(WaitLevelLoadOver());
            //    }
            //});
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


