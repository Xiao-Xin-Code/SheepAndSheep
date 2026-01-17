
using Frame;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class LevelController : MonoController
    {
        LevelSystem _levelSystem;
		AssetSystem _assetSystem;
		DataModel _dataModel;
		LevelModel _levelModel;

        public override void Init()
        {
			_levelModel = this.GetModel<LevelModel>();
			_dataModel = this.GetModel<DataModel>();
            _levelSystem = this.GetSystem<LevelSystem>();
			_assetSystem = this.GetSystem<AssetSystem>();
			this.RegisterEvent<LaunchLevelEvent>(OnLaunchLevel);
		}


        private void OnLaunchLevel(LaunchLevelEvent evt)
		{
			//初始化数据
			_levelModel.isLevelOver = false;
			string[] level = _assetSystem.GetLevel();
			Debug.Log(level.Length + level[0]);
			Debug.Log(level[0].Split('#').Length);
			string[] split = level[0].Split('#')[1].Split('|');
			_levelModel.blockCount = int.Parse(split[0]);
			_levelModel.width = int.Parse(split[1].Split(',')[0]);
			_levelModel.height = int.Parse(split[1].Split(',')[1]);
			_levelModel.center = new Vector2(int.Parse(split[2].Split(',')[0]), int.Parse(split[2].Split(',')[1]));
			_levelModel.level = level;
			//加载图标数据
			LoadBlockIcons();
			//初始类型数据
			_levelSystem.InitBlockTypes();
			//初始化关卡
			this.SendCommand<InitLevelCommand>();
		}

		/// <summary>
		/// 加载图标的数据
		/// </summary>
		private void LoadBlockIcons()
		{
			Sprite[] icons = Resources.LoadAll<Sprite>(_dataModel.Theme.Value);
			foreach (var item in icons)
			{
				Debug.Log("添加：" + item);
				_assetSystem.AddIcon(item.name, item);
			}
		}

	}
}