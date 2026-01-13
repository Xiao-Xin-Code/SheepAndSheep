using UnityEngine;

namespace Sheep
{
    public class GridsController : MonoController
    {
        GridController[][] grids;

        public override void Init()
        {
            
        }


        /// <summary>
        /// 初始化底盘
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="center"></param>
        private void InitializeGrids(int width,int height,Vector2 center)
        {
            grids = new GridController[height][];
            


        }


        /// <summary>
        /// 放置指定
        /// </summary>
        /// <param name="block"></param>
        private void Place(BlockController block)
        {
            Vector2 position = Vector2.zero;
            foreach (var item in block.OccupiedCells) 
            {
                GridController grid = grids[item.x][item.y];
                BlockController temp = grid.Peek();
                if (temp != null) temp.Interactable = false;
                grid.Push(block);
				position += grid.Location;
			}
            position /= 4;

        }

        /// <summary>
        /// 移除指定
        /// </summary>
        /// <param name="block"></param>
        private void Remove(BlockController block)
        {
            foreach (var item in block.OccupiedCells) 
            {
                grids[item.x][item.y].Pop();
                GridController grid = grids[item.x][item.y];
                BlockController temp = grid.Peek();
                if (temp != null) temp.Interactable = CheckInteractable(temp);
            }
        }

        /// <summary>
        /// 检查交互性
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>

        private bool CheckInteractable(BlockController block)
        {
            foreach (var item in block.OccupiedCells) 
            {
                BlockController temp = grids[item.x][item.y].Peek();
                if (temp != block) return false;
            }
            return true;

        }


    }
}


