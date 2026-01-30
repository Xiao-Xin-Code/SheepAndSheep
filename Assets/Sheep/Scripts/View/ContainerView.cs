using UnityEngine;

namespace Sheep
{
    public class ContainerView : BaseView
    {

        [SerializeField] RectTransform[] cells = new RectTransform[7];
        [SerializeField] RectTransform[] extendCells = new RectTransform[3];

        public RectTransform[] Cells => cells;
        public RectTransform[] ExtendCells => extendCells;

    }

}


