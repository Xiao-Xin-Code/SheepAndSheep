using System.Collections.Generic;
using System.IO;
using DG.Tweening.Plugins.Core.PathCore;
using QMVC;
using UnityEngine;

namespace Sheep
{

    public class AssetSystem : AbstractSystem
    {
        public BlockController block;
        public SFXController sfx;
		private Dictionary<string, Sprite> themeIcons = new Dictionary<string, Sprite>();
		string[] levelPaths;


		protected override void OnInit()
        {
            sfx = Resources.Load<SFXController>("SFX");
            block = Resources.Load<BlockController>("Item");

			string levelPath = Application.streamingAssetsPath + "/Level";
			levelPaths = Directory.GetFiles(levelPath,"*.txt");
			
        }


		public string[] GetLevel()
		{
			int index = Random.Range(0, levelPaths.Length);
			string[] lines = File.ReadAllLines(levelPaths[index]);
			return lines;
		}

		private void ReadLevel()
		{
			string path = "";

			string[] lines = File.ReadAllLines(path);

			foreach (var line in lines)
			{
				string head = line.Split('#')[0];

				if(head == "T")
				{
					string[] datas = line.Split('#')[1].Split('|');

					int.Parse(datas[0]);
					int.Parse(datas[1].Split(',')[0]);
					int.Parse(datas[1].Split(',')[1]);
					int.Parse(datas[2].Split(',')[1]);
					int.Parse(datas[2].Split(',')[1]);


				}
				else if (head == "1")
				{

				}
				else if(head == "2")
				{

				}

			}


		}



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
	}

}


