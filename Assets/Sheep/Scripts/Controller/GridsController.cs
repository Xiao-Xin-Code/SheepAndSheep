using QMVC;
using UnityEngine;

namespace Sheep
{
    public class GridsController : BaseController
    {
        GridController[][] grids;

        LevelModel _levelModel;
        LevelSystem _levelSystem;
        PoolSystem _poolSystem;


        public GridsController()
        {
			_levelModel = this.GetModel<LevelModel>();
            _levelSystem = this.GetSystem<LevelSystem>();
			_poolSystem = this.GetSystem<PoolSystem>();
            _levelModel.width = 20;
            _levelModel.height = 20;
			this.RegisterEvent<InitLevelEvent>(OnInitLevel);
            this.RegisterEvent<RemoveInGridsEvent>(OnRemoveInGrids);
		}


        private void OnInitLevel(InitLevelEvent evt)
        {
            Debug.Log("初始化");
            //获取数量，
            //随机种类
            //确定种类包含的数量
            //设置实际效果



			InitializeGrids(_levelModel.width, _levelModel.height, _levelModel.center);

            CreateHeadUpBlock(2, new Vector2Int(0, 0), new Vector2(0, -0.15f), 1);
            CreateHeadUpBlock(2, new Vector2Int(0, 4), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(0, 8), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(4, 0), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(4, 4), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(4, 8), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(8, 0), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(8, 4), new Vector2(0, -0.15f), 1);
            //CreateHeadUpBlock(2, new Vector2Int(8, 8), new Vector2(0, -0.15f), 1);


            _levelModel.isLevelOver = true;
		}


        private void OnRemoveInGrids(RemoveInGridsEvent evt)
        {
            Remove(evt.block);
		}

        private void Refresh()
        {
            int total = 0;
            int[] types = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

            while (total < _levelModel.blockCount)
            {
                int index = Random.Range(0, 7);
                types[index] += 3;
                total += 3;
            }

            Random.Range(4, 8);
        }

        int id = 0;

        /// <summary>
        /// 初始化底盘
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="center"></param>
        private void InitializeGrids(int width,int height,Vector2 center)
        {
			Transform block = Resources.Load<Transform>("Grid");

			grids = new GridController[height][];

			float startY = height * 0.6f / 2;
			float startX = -width * 0.6f / 2;

			for (int h = 0; h < height; h++)
			{
				float curY = startY - h * 0.6f;
				grids[h] = new GridController[width];
				for (int w = 0; w < width; w++)
				{
					float curX = startX + w * 0.6f;
					grids[h][w] = new GridController(new Vector2(curX, curY));
                    //Transform temp = GameObject.Instantiate(block);
					//temp.position = new Vector2(curX, curY);
				}

			}
		}


        /// <summary>
        /// 放置指定
        /// </summary>
        /// <param name="block"></param>
        private void Place(BlockController block,int deep)
        {
            Vector2 position = Vector2.zero;
            foreach (var item in block.OccupiedCells) 
            {
                GridController grid = grids[item.x][item.y];
                BlockController temp = _levelSystem.GetBlock(grid.Peek());
				if (temp != null) temp.interactable = false;
                int id = this.id++;//获取ID
                grid.Push(id);
                _levelSystem.AddBlock(id, block);
				position += grid.Location;
			}
            position /= 4;
            block.transform.position = new Vector3(position.x, position.y, deep);
        }

        /// <summary>
        /// 移除指定
        /// </summary>
        /// <param name="block"></param>
        private void Remove(BlockController block)
        {
            foreach (var item in block.OccupiedCells) 
            {
                int id = grids[item.x][item.y].Pop();
                _levelSystem.RemoveBlock(id);
                GridController grid = grids[item.x][item.y];
                BlockController temp = _levelSystem.GetBlock(grid.Peek());
				if (temp != null) temp.interactable = CheckInteractable(temp);
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
                BlockController temp = _levelSystem.GetBlock(grids[item.x][item.y].Peek());
                if (temp != block) return false;
            }
            return true;

        }

        /// <summary>
        /// 指定范围区块
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="startCoord"></param>
        /// <param name="deep"></param>
		public void CreateSpecifiedBlock(int width, int height, Vector2Int startCoord, int deep)
		{
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					//创建block
					BlockController block = _poolSystem.GetBlock();
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

        /// <summary>
        /// 单一重叠区块
        /// </summary>
        /// <param name="count"></param>
        /// <param name="startCoord"></param>
        /// <param name="dur"></param>
        /// <param name="startDeep"></param>
		public void CreateHeadUpBlock(int count, Vector2Int startCoord, Vector3 dur, int startDeep)
		{
			for (int i = 0; i < count; i++)
			{
				BlockController block = _poolSystem.GetBlock();
				block.OccupiedCells = new Vector2Int[4]
				{
					startCoord,
					startCoord + new Vector2Int(0,1),
					startCoord + new Vector2Int(1,0),
					startCoord + new Vector2Int(1,1)
				};

				block.interactable = true;

                Place(block, startDeep - i);
				block.transform.position += dur * (count - 1 - i);
			}
		}
	}
}


