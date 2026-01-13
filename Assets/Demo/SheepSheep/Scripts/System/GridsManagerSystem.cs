using QFramework;
using UnityEngine;

namespace SheepSheep
{
    public class GridsManagerSystem : AbstractSystem
    {
        GridController[][] grids;

        PoolSystem poolSystem;


        protected override void OnInit()
        {
            poolSystem = this.GetSystem<PoolSystem>();

            InitalizeGrids(20, 24, Vector2.zero);

            Debug.Log("生成");
            //CreateSpecifiedBlock(2, 2, new Vector2Int(0, 0), 1);

            //CreateHeadUpBlock(2, new Vector2Int(0, 0), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(0, 4), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(0, 8), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(4, 0), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(4, 4), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(4, 8), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(8, 0), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(8, 4), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(8, 8), new Vector2(0, -0.15f), 1);
        }

        //-5.7f,-5.4f,-4.8f,-4.2f,-4.

        public void InitalizeGrids(int width, int height, Vector2 center)
        {
            grids = new GridController[height][];
            
            
            float startY = height * 0.6f / 2 - 0.3f;
            float startX = -width * 0.6f / 2 + 0.3f;


            for (int h = 0; h < height; h++)
            {
                float curY = startY - h * 0.6f;
                grids[h] = new GridController[width];
                for (int w = 0; w < width; w++)
                {
                    float curX = startX + w * 0.6f;
                    grids[h][w] = new GridController(new Vector2(curX, curY));
                    //BlockController block = poolSystem.GetBlock();
                    //block.transform.position = new Vector3(curX, curY, -1);
                }
            }
        }


        //    ,-0.6f,0.6f,1.8f,3f,4.2f,5.4f




        public void Place(BlockController block, int deep)
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
            block.transform.position = new Vector3(position.x, position.y, deep);
        }

        public void Remove(BlockController block)
        {
            foreach (var item in block.OccupiedCells) 
            {
                grids[item.x][item.y].Pop();
                GridController grid = grids[item.x][item.y];
                BlockController temp = grid.Peek();
                if (temp != null) temp.Interactable = CheckInteractable(temp);
            }
        }

        private bool CheckInteractable(BlockController block)
        {
            foreach (var item in block.OccupiedCells)
            {
                BlockController temp = grids[item.x][item.y].Peek();
                if (temp != block) return false;
            }
            return true;
        }






        public void CreateSpecifiedBlock(int width, int height, Vector2Int startCoord, int deep)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //创建block
                    BlockController block = poolSystem.GetBlock();
                    block.OccupiedCells = new Vector2Int[4]
                    {
                        startCoord + new Vector2Int(i * 2,j * 2),
                        startCoord + new Vector2Int(i * 2,j * 2) + new Vector2Int(0,1),
                        startCoord + new Vector2Int(i * 2,j * 2) + new Vector2Int(1,0),
                        startCoord + new Vector2Int(i * 2,j * 2) + new Vector2Int(1,1)
                    };

                    Place(block, deep);
                }
            }




        }


        public void CreateHeadUpBlock(int count, Vector2Int startCoord, Vector3 dur, int startDeep)
        {
            for (int i = 0; i < count; i++)
            {
                BlockController block = poolSystem.GetBlock();
                block.OccupiedCells = new Vector2Int[4]
                {
                    startCoord,
                    startCoord + new Vector2Int(0,1),
                    startCoord + new Vector2Int(1,0),
                    startCoord + new Vector2Int(1,1)
                };

                block.Interactable = true;

                Place(block, startDeep - i);
                block.transform.position += dur * (count - 1 - i);
            }
        }

    }
}

