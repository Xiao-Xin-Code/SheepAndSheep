using QMVC;
using UnityEngine;

namespace Sheep
{
    public enum LevelState
    {
        Runtime,
        Over,
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

		protected override void OnInit()
        {
			levelState = new BindableProperty<LevelState>();
			levelState.Register(OnLevelStateChanged);
        }

        private void OnLevelStateChanged(LevelState levelState)
        {
            switch (levelState)
            {
                case LevelState.Runtime:
                    break;
                case LevelState.Over:
                    break;
                case LevelState.Succeed:
                    break;
                default:
                    break;
            }
        }
    }
}


