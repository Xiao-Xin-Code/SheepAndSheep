using System.Collections.Generic;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class LevelSystem : AbstractSystem
    {
        /// <summary>
        /// 关卡中当前存在block
        /// </summary>
        private Dictionary<int, BlockController> blocks = new Dictionary<int, BlockController>();
        private List<string> blockTypes = new List<string>();

		LevelModel _levelModel;

        protected override void OnInit()
        {
			_levelModel = this.GetModel<LevelModel>();
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

		public void ClearBlocks()
		{
			blocks.Clear();
		}



		/// <summary>
		/// 洗牌
		/// </summary>
		public void Shuffle()
		{
			System.Random random = new System.Random();
			int n = blockTypes.Count - 1;
			while (n > 0)
			{
				var temp = random.Next(n + 1);
				(blockTypes[n], blockTypes[temp]) = (blockTypes[temp], blockTypes[n]);
				n--;
			}
		}

		/// <summary>
		/// 初始化Block类型数据
		/// </summary>
		public void InitBlockTypes()
		{
			int[] types = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
			int total = 0;
			while (total < _levelModel.blockCount)
			{
				int index = Random.Range(0, 7);
				types[index] += 3;
				total += 3;
			}
			blockTypes = new List<string>();
			for (int i = 0; i < types.Length; i++)
			{
				for (int c = 0; c < types[i]; c++)
				{
					blockTypes.Add($"block_{i + 1}");
				}
			}
			Debug.Log(blockTypes.Count);
			Shuffle();
		}


		/// <summary>
		/// 统一更新当前block
		/// </summary>
		public void UpdateBlock()
		{
			int index = 0;
			Debug.Log(blocks.Count);
			foreach (var item in blocks)
			{
				Debug.Log("当前Index" + index);
				item.Value.content = blockTypes[index];
				Debug.Log(blockTypes[index]);
				item.Value.UpdateIcon();
				index++;
			}
		}

	}
}


