using System.Collections.Generic;
using System.IO;
using QMVC;
using UnityEngine;

namespace Sheep
{
	public class LevelModel : AbstractModel
    {
        public bool isLevelOver = false;

        public int blockCount;
        public int width;
        public int height;
        public Vector2 center;

        public string[] level;


		protected override void OnInit()
        {
            
            
        }
    }
}


