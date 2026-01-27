using QMVC;
using System;
using UnityEngine;

namespace Sheep
{
    public class LevelController : MonoController
    {
		[SerializeField] LevelView _levelView;

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
			_levelView.RegisterSetPressed(OnLevelSetPressed);

			_levelView.gameObject.SetActive(false);
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

			//更新UI
			_levelView.SetDate(DateTime.Now.ToString("- MM月dd日 -"));


			//初始化关卡
			this.SendCommand<InitLevelCommand>();
		}


		private void OnLevelStateChanged(LevelState levelState)
		{
			switch (levelState)
			{
				case LevelState.Runtime:
					break;
				case LevelState.FailureWithResurrection:
					this.SendCommand(new MaskVisibleCommand(true));

					this.SendCommand(new GameOverCommand(true));
					break;
				case LevelState.Failure:
					this.SendCommand(new MaskVisibleCommand(true));

					//更新成就AchievementSystem

					this.SendCommand(new GameOverCommand(false));
					break;
				case LevelState.Succeed:
                    this.SendCommand(new MaskVisibleCommand(true));

					//更新成就AchievementSystem

					this.SendCommand<GameSucceedCommand>();
					break;
				default:
					break;
			}
		}


		private void OnLevelSetPressed()
		{
			this.SendCommand(new LevelSetVisibleCommand(true));
		}

		private void LevelVisible(LevelVisibleEvent evt)
		{
			gameObject.SetActive(evt.visible);
            _levelView.gameObject.SetActive(evt.visible);
        }

	}
}