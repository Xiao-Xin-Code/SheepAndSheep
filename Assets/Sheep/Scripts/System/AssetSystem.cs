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
		private Dictionary<string, Sprite> themeIcons;

		DataModel _dataModel;

		protected override void OnInit()
        {
			_dataModel = this.GetModel<DataModel>();

			_dataModel.Theme.RegisterWithInitValue(OnThemeChanged);

			sfx = Resources.Load<SFXController>("SFX");
            block = Resources.Load<BlockController>("Item");

			string levelPath = Application.streamingAssetsPath + "/Level";
			_dataModel.levelPaths = Directory.GetFiles(levelPath,"*.txt");
		}

		/// <summary>
		/// 主题变化
		/// </summary>
		/// <param name="theme"></param>
		private void OnThemeChanged(string theme)
		{
			themeIcons = new Dictionary<string, Sprite>();
			Sprite[] icons = Resources.LoadAll<Sprite>(_dataModel.Theme.Value);
			foreach (var item in icons)
			{
				Debug.Log("添加：" + item);
				AddIcon(item.name, item);
			}
		}

		#region 关卡数据操作

		public string[] GetLevel()
		{
			int index = Random.Range(0, _dataModel.levelPaths.Length);
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


