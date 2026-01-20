using QMVC;
using UnityEngine;

namespace Sheep
{
    public enum LevelState
    {
        Runtime,
        Failure,
        Succeed
    }


	public class LevelModel : AbstractModel
    {
        public bool isLevelOver = false;

        public int blockCount;
        public int width;
        public int height;
        public Vector2 center;
        public string[] level;
        public BindableProperty<LevelState> levelState;

        public bool levelup = false;

		protected override void OnInit()
        {
			levelState = new BindableProperty<LevelState>();
        }

    
    }
}


