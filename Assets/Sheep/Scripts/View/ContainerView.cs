using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sheep
{
    public class ContainerView : BaseView
    {

        [SerializeField] Transform[] cells = new Transform[7];

        public Transform[] Cells => cells;

    }

}


