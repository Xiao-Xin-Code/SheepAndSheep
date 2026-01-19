using QMVC;
using UnityEngine;

namespace Sheep
{
    public class GridsController : MonoController
    {
        GridController[][] grids;

        LevelModel _levelModel;
        LevelSystem _levelSystem;
        PoolSystem _poolSystem;

		public override void Init()
		{
			_levelModel = this.GetModel<LevelModel>();
			_levelSystem = this.GetSystem<LevelSystem>();
			_poolSystem = this.GetSystem<PoolSystem>();
			this.RegisterEvent<InitLevelEvent>(OnInitLevel);
			this.RegisterEvent<RemoveInGridsEvent>(OnRemoveInGrids);
		}


		private void OnInitLevel(InitLevelEvent evt)
        {
            Debug.Log("初始化");
			InitializeGrids(_levelModel.width, _levelModel.height, _levelModel.center);

            for(int i = 1; i < _levelModel.level.Length; i++)
            {
                CreateLevelBlock(_levelModel.level[i]);
			}

            _levelSystem.UpdateBlock();
            _levelModel.isLevelOver = true;
		}

        private void CreateLevelBlock(string areadata)
        {
            string[] split = areadata.Split('#');
            string[] data = split[1].Split('|');
            switch (split[0])
            {
                case "1":
                    string[] v2Int = data[1].Split(',');
                    string[] v2 = data[2].Split(',');
                    CreateHeadUpBlock(int.Parse(data[0]), new Vector2Int(int.Parse(v2Int[0]), int.Parse(v2Int[1])), new Vector2(float.Parse(v2[0]), float.Parse(v2[1])), int.Parse(data[3]));
					break;
                case "2":
                    string[] v2Int_wh = data[0].Split(',');
                    v2Int = data[1].Split(',');
                    CreateSpecifiedBlock(int.Parse(v2Int_wh[0]), int.Parse(v2Int_wh[1]), new Vector2Int(int.Parse(v2Int[0]), int.Parse(v2Int[1])), int.Parse(data[2]));
					break;
            }
        }

        private void OnRemoveInGrids(RemoveInGridsEvent evt)
        {
            Remove(evt.block);
		}


		#region Grids操作方法

		int id = 0;
		/// <summary>
		/// 初始化底盘
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="center"></param>
		private void InitializeGrids(int width, int height, Vector2 center)
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
		private void Place(BlockController block, int deep)
		{
			Vector2 position = Vector2.zero;
			int id = this.id++;//获取ID
			foreach (var item in block.OccupiedCells)
			{
				GridController grid = grids[item.x][item.y];
				BlockController temp = _levelSystem.GetBlock(grid.Peek());
				if (temp != null) temp.interactable = false;

				grid.Push(id);

				position += grid.Location;
			}
			_levelSystem.AddBlock(id, block);
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

		#endregion

		#region 区域创建方法

		/// <summary>
		/// 指定范围区块
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="startCoord"></param>
		/// <param name="deep"></param>
		private void CreateSpecifiedBlock(int width, int height, Vector2Int startCoord, int deep)
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
					Debug.Log("添加：" + i + "|" + j);
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
		private void CreateHeadUpBlock(int count, Vector2Int startCoord, Vector3 dur, int startDeep)
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

		#endregion

	}
}


