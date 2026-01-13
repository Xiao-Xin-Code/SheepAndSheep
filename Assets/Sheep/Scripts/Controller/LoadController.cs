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

        public override void Init()
        {
            GenerateCircularPath();
            _view.SheepTransform.position = path[0];
            _view.SheepTransform.DOPath(path, duration).SetOptions(true).SetEase(Ease.Linear).OnUpdate(UpdateSheepDirection).OnComplete(() => gameObject.SetActive(false));
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

            bool isInLeftHalf = currentPosition.y < centerPosition.y;

            float targetScaleX = isInLeftHalf ? -1f : 1f;

            Vector3 currentScale = _view.SheepTransform.localScale;
            _view.SheepTransform.localScale = new Vector3(targetScaleX * currentScale.x, currentScale.y, currentScale.z); //new Vector3(targetScaleX, currentPosition.y, currentPosition.z);
        }

        
    }
}


