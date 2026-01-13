using DG.Tweening;
using UnityEngine;

namespace SheepSheep
{
    public class LoadController : MonoController
    {
        RectTransform slider;

        float radius = 300;
        int pathPoints = 20;
        float duration = 5;
        bool loop = true;

        public Transform sheepTransform;

        private Vector3[] path;


        protected override void Init()
        {
            GenerateCircularPath();
            sheepTransform.position = path[0];
            sheepTransform.DOPath(path, duration).SetOptions(true).SetEase(Ease.Linear).SetLoops(-1).OnUpdate(UpdateSheepDirection);
        }




        void GenerateCircularPath()
        {
            path = new Vector3[pathPoints];

            for (int i = 0; i < pathPoints; i++)
            {
                float angle = i * (360f / pathPoints) * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;

                path[i] = transform.TransformPoint(new Vector3(x, y, 0));
            }
        }


        void UpdateSheepDirection()
        {
            if (sheepTransform == null) return;

            Vector3 currentPosition = sheepTransform.position;
            Vector3 centerPosition = transform.position;

            // 判断是否在左半圆（X坐标小于中心点）
            bool isInLeftHalf = currentPosition.y < centerPosition.y;

            // 根据左右半圆设置水平翻转
            float targetScaleX = isInLeftHalf ? -1f : 1f;

            // 应用水平翻转
            Vector3 currentScale = sheepTransform.localScale;
            sheepTransform.localScale = new Vector3(targetScaleX, currentScale.y, currentScale.z);
        }

    }
}


