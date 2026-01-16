
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class LevelController : MonoController
    {
		LevelModel _levelModel;
        LevelSystem _levelSystem;
		AssetSystem _assetSystem;

        public override void Init()
        {
			_levelModel = this.GetModel<LevelModel>();
            _levelSystem = this.GetSystem<LevelSystem>();
			_assetSystem = this.GetSystem<AssetSystem>();
			this.RegisterEvent<LaunchLevelEvent>(OnLaunchLevel);
		}

		private void OnLaunchLevel(LaunchLevelEvent evt)
		{
			//先加载关卡数据
			OnInitLevelModel();
			//初始化关卡
			this.SendCommand<InitLevelCommand>();
		}

		private void OnInitLevelModel()
		{
			//加载assets
			_levelModel.Theme.Value = "Theme0";
			_levelModel.blockCount = 6;
			LoadBlockIcons();
			//初始类型数据
			_levelSystem.InitBlockTypes();
		}

		private void LoadBlockIcons()
		{
			Sprite[] icons = Resources.LoadAll<Sprite>(_levelModel.Theme.Value);
			foreach (var item in icons)
			{
				Debug.Log("添加：" + item);
				_assetSystem.AddIcon(item.name, item);
			}
		}

	}
}