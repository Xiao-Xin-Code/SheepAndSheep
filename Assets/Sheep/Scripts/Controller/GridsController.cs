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

		[SerializeField] GridsView _gridsView;




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
			InitializeGrids(_levelModel.width, _levelModel.height, _levelModel.center);
			id = 0;
			for (int i = 1; i < _levelModel.level.Length; i++)
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
                    string[] startCoord = data[1].Split(',');
                    string[] dur = data[2].Split(',');
					CreateHeadUpBlock(int.Parse(data[0]), new Vector2Int(int.Parse(startCoord[0]), int.Parse(startCoord[1])), new Vector2(float.Parse(dur[0]), float.Parse(dur[1])));
					break;
                case "2":
                    string[] size = data[0].Split(',');
					startCoord = data[1].Split(',');
					CreateSpecifiedBlock(int.Parse(size[0]), int.Parse(size[1]), new Vector2Int(int.Parse(startCoord[0]), int.Parse(startCoord[1])));
					break;
            }
        }

        private void OnRemoveInGrids(RemoveInGridsEvent evt)
        {
			if (_levelSystem.GetBlock(evt.block.ID))
			{
				Remove(evt.block);
			}
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
			grids = new GridController[height][];
			BlockController block = _poolSystem.GetBlock();
			float size = block.RectTransform.rect.width;
			_poolSystem.RecycleBlock(block);
			float needSize = size / 2;
			float halfSize = needSize / 2;
			int halfcount_h = height / 2;
			int halfcount_w = width / 2;
			int modh = height % 2;
			int modw = width % 2;

			float startY = halfcount_h * needSize - (modh == 0 ? halfSize : 0);
			float startX = -halfcount_w * needSize + (modw == 0 ? halfSize : 0);

			

			for(int h = 0; h < height; h++)
			{
				float curY = startY - h * needSize;
				grids[h] = new GridController[width];
				for (int w = 0; w < width; w++)
				{
					float curX = startX + w * needSize;
					grids[h][w] = new GridController(new Vector2(curX, curY));
				}
			}
		}

		/// <summary>
		/// 放置指定
		/// </summary>
		/// <param name="block"></param>
		private void Place(BlockController block)
		{
			Vector2 position = Vector2.zero;
			int id = this.id++;//获取ID
			block.ID = id;
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

			block.RectTransform.anchoredPosition = position;
		}

		/// <summary>
		/// 移除指定
		/// </summary>
		/// <param name="block"></param>
		private void Remove(BlockController block)
		{
			int blockId = -1;
            foreach (var item in block.OccupiedCells)
			{
                blockId = grids[item.x][item.y].Pop();
				GridController grid = grids[item.x][item.y];
				BlockController temp = _levelSystem.GetBlock(grid.Peek());
				if (temp != null) temp.interactable = CheckInteractable(temp);
			}
            _levelSystem.RemoveBlock(blockId);
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
		private void CreateSpecifiedBlock(int width, int height, Vector2Int startCoord)
		{
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					//创建block
					BlockController block = _poolSystem.GetBlock();
					block.RectTransform.SetParent(transform, false);
					block.interactable = true;
					block.OccupiedCells = new Vector2Int[4]
					{
						startCoord + new Vector2Int(i * 2,j * 2),
						startCoord + new Vector2Int(i * 2,j * 2) + new Vector2Int(0,1),
						startCoord + new Vector2Int(i * 2,j * 2) + new Vector2Int(1,0),
						startCoord + new Vector2Int(i * 2,j * 2) + new Vector2Int(1,1)
					};

                    Place(block);
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
		private void CreateHeadUpBlock(int count, Vector2Int startCoord, Vector2 dur)
		{
			for (int i = 0; i < count; i++)
			{
				BlockController block = _poolSystem.GetBlock();
				block.RectTransform.SetParent(transform, false);
                block.interactable = true;
                block.OccupiedCells = new Vector2Int[4]
				{
					startCoord,
					startCoord + new Vector2Int(0,1),
					startCoord + new Vector2Int(1,0),
					startCoord + new Vector2Int(1,1)
				};

				Place(block);

				//单独设置偏移
				Debug.Log("调整前" + block.RectTransform.anchoredPosition);
				block.RectTransform.anchoredPosition += dur * (count - 1 - i);
				Debug.Log("调整后" + block.RectTransform.anchoredPosition);
			}
		}

		#endregion

	}
}


