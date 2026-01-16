using System.Collections.Generic;
using QMVC;
using UnityEngine;

namespace Sheep
{

    public class AssetSystem : AbstractSystem
    {
        public BlockController block;
        public SFXController sfx;
		private Dictionary<string, Sprite> themeIcons = new Dictionary<string, Sprite>();



		protected override void OnInit()
        {
            sfx = Resources.Load<SFXController>("SFX");
            block = Resources.Load<BlockController>("Item");

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


