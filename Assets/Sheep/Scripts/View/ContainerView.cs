using UnityEngine;

namespace Sheep
{
    public class ContainerView : BaseView
    {

        [SerializeField] Transform[] cells = new Transform[7];
        [SerializeField] Transform[] extendCells = new Transform[3];

        public Transform[] Cells => cells;
        public Transform[] ExtendCells => extendCells;

    }

}


