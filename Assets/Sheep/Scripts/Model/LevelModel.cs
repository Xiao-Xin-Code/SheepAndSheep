using QMVC;
using UnityEngine;

namespace Sheep
{
    public class LevelModel : AbstractModel
    {
        public bool isLevelOver = false;
        public BindableProperty<string> Theme = new BindableProperty<string>();


        public int blockCount;
        public int width;
        public int height;
        public Vector2 center;
        

        protected override void OnInit()
        {
            
        }
    }
}


