using System.Collections.Generic;
using System.Diagnostics;
using QMVC;

namespace Sheep
{
    public class LevelSystem : AbstractSystem
    {
        /// <summary>
        /// 关卡中当前存在block
        /// </summary>
        private Dictionary<int, BlockController> blocks = new Dictionary<int, BlockController>();

        protected override void OnInit()
        {

		}


        public BlockController GetBlock(int id)
        {
            blocks.TryGetValue(id, out BlockController value);
            return value;
        }

        public void AddBlock(int id, BlockController block)
        {
            if (!blocks.ContainsKey(id))
            {
                blocks.Add(id, block);
			}
        }

        public void RemoveBlock(int id)
        {
            if (blocks.ContainsKey(id))
            {
				blocks.Remove(id);
			}
        }

        public bool HasBlocks()
        {
            return blocks.Count != 0;
        }
    }
}


