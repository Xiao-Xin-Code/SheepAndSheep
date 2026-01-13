using QMVC;
using UnityEngine;
using UnityEngine.UI;

namespace Sheep
{
    public class LoadView : BaseView
    {
        [SerializeField] Image progress;
        [SerializeField] RectTransform sheepTransform;

        public RectTransform SheepTransform => sheepTransform;

    }
}


