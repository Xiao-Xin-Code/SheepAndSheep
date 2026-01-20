using System.Collections.Generic;
using System.IO;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class AssetSystem : AbstractSystem
    {
        public BlockController block;
        public SFXController sfx;

		private Sprite bgSprite;
		private Sprite maskSprite;
		private Dictionary<string, Sprite> themeIcons;

		DataModel _dataModel;

		public Sprite BgSprite { get => bgSprite; }
		public Sprite MaskSprite { get => maskSprite; }

		protected override void OnInit()
        {
			_dataModel = this.GetModel<DataModel>();

			_dataModel.Theme.RegisterWithInitValue(OnThemeChanged);

			sfx = Resources.Load<SFXController>("SFX");
            block = Resources.Load<BlockController>("Item");
			
			
			string levelPath = Application.streamingAssetsPath + "/Level";
			_dataModel.levelPaths = Directory.GetFiles(levelPath,"*.txt");

			GameConfig config = Resources.Load<GameConfig>("GameConfig");
			_dataModel.MusicIsOn.Value = config.MusicIsOn;
			_dataModel.SfxIsOn.Value = config.SfxIsOn;
			_dataModel.ShakeIsOn.Value = config.ShakeIsOn;
			_dataModel.Theme.Value = config.Theme;
		}

		/// <summary>
		/// 主题变化
		/// </summary>
		/// <param name="theme"></param>
		private void OnThemeChanged(string theme)
		{
			bgSprite = Resources.Load<Sprite>($"{_dataModel.Theme.Value}/block_bg");
			maskSprite = Resources.Load<Sprite>($"{_dataModel.Theme.Value}/blackMask");

			if (themeIcons == null)
			{
				themeIcons = new Dictionary<string, Sprite>();
			}
			else
			{
				themeIcons.Clear();
			}

			Sprite[] icons = Resources.LoadAll<Sprite>($"{_dataModel.Theme.Value}/Block");
			foreach (var item in icons)
			{
				AddIcon(item.name, item);
			}
		}

		#region 关卡数据操作

		public string[] GetDefaultLevel()
		{
			string[] lines = File.ReadAllLines(_dataModel.levelPaths[0]);
			return lines;
		}

		public string[] GetLevel()
		{
			int index = Random.Range(1, _dataModel.levelPaths.Length);
			string[] lines = File.ReadAllLines(_dataModel.levelPaths[index]);
			return lines;
		}

		#endregion

		#region 图标数据操作

		public Sprite GetIcon(string path)
		{
			if (themeIcons.ContainsKey(path))
			{
				return themeIcons[path];
			}
			return null;
		}

		public void AddIcon(string path, Sprite sprite)
		{
			if (!themeIcons.ContainsKey(path))
			{
				themeIcons.Add(path, sprite);
			}
		}

		public void RemoveIcon()
		{

		}

		public void ClearIcons()
		{
			themeIcons.Clear();
		}

		#endregion

	}

}


