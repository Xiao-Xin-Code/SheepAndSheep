using System.Collections.Generic;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class LevelModel : AbstractModel
    {
        public bool isLevelOver = false;

        public int blockCount;
        public List<int> types;

        public int width;
        public int height;
        public Vector2 center;
        

        protected override void OnInit()
        {
            
        }
    }
}


