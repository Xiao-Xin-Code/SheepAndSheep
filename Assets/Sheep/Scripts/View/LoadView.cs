using QMVC;
using UnityEngine;

namespace Sheep
{
    public class LoadView : BaseView
    {
        [SerializeField] RectTransform progress;
        [SerializeField] RectTransform sheepTransform;

        public RectTransform Progress => progress;
        public RectTransform SheepTransform => sheepTransform;

    }
}


