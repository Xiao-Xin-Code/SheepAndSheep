using Frame;
using QMVC;
using UnityEditor;
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
			this.RegisterEvent<LevelVisibleEvent>(LevelVisible);

			_levelModel.levelState.Register(OnLevelStateChanged);

			gameObject.SetActive(false);

		}


        private void OnLaunchLevel(LaunchLevelEvent evt)
		{
			//初始化数据
			_levelModel.isLevelOver = false;
			string[] level = _levelModel.levelup ? _assetSystem.GetLevel() : _assetSystem.GetDefaultLevel();
			string[] split = level[0].Split('#')[1].Split('|');
			_levelModel.blockCount = int.Parse(split[0]);
			_levelModel.width = int.Parse(split[1].Split(',')[0]);
			_levelModel.height = int.Parse(split[1].Split(',')[1]);
			_levelModel.center = new Vector2(int.Parse(split[2].Split(',')[0]), int.Parse(split[2].Split(',')[1]));
			_levelModel.level = level;
			_levelModel.levelState.Value = LevelState.Runtime;
			//初始类型数据
			_levelSystem.InitBlockTypes();
			//初始化关卡
			this.SendCommand<InitLevelCommand>();
		}


		private void OnLevelStateChanged(LevelState levelState)
		{
			switch (levelState)
			{
				case LevelState.Runtime:
					break;
				case LevelState.Failure:
					Debug.Log("触发失败");
					this.SendCommand<GameOverCommand>();
					break;
				case LevelState.Succeed:
					this.SendCommand<GameSucceedCommand>();
					break;
				default:
					break;
			}
		}


		private void LevelVisible(LevelVisibleEvent evt)
		{
			gameObject.SetActive(evt.visible);
		}

	}
}