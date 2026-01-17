using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Sheep
{
    public class LoadController : MonoController
    {
        [SerializeField] LoadView _view;

        private float radius = 300;
        private int pathPoints = 20;
		float duration = 5;
		private Vector3[] path;

        Coroutine coroutine;

        public override void Init()
        {
            GenerateCircularPath();
            BeginProgress();

		}


        void GenerateCircularPath()
        {
            path = new Vector3[pathPoints];
            for (int i = 0; i < pathPoints; ++i) 
            {
                float angle = i * (360f / pathPoints) * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;
                path[i] = transform.TransformPoint(new Vector3(x, y, 0));
            }
        }

        void UpdateSheepDirection()
        {
            if (_view.SheepTransform == null) return;

            Vector3 currentPosition = _view.SheepTransform.position;
            Vector3 centerPosition = transform.position;

            // 判断是否在左半圆（X坐标小于中心点）
            bool isInLeftHalf = currentPosition.y < centerPosition.y;

            // 根据左右半圆设置水平翻转
            float targetScaleX = isInLeftHalf ? -1f : 1f;

            // 应用水平翻转
            Vector3 currentScale = _view.SheepTransform.localScale;
            _view.SheepTransform.localScale = new Vector3(targetScaleX, currentScale.y, currentScale.z);
        }


        void BeginProgress()
        {
            DOTween.Kill(_view.SheepTransform);
			_view.SheepTransform.position = path[0];
			_view.SheepTransform.DOPath(path, duration).SetOptions(true).SetEase(Ease.Linear).SetLoops(-1).OnUpdate(UpdateSheepDirection);
			if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(LoadProgress());
        }



        IEnumerator LoadProgress()
        {
            while (_view.Progress.rect.width < 1000) 
            {
                _view.Progress.sizeDelta += new Vector2(500, 0) * Time.deltaTime;
				yield return null;
			}

            gameObject.SetActive(false);
        }

    }
}


