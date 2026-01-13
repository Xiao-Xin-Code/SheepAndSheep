using QMVC;
using UnityEngine;

namespace SheepSheep
{
    public class AssetSystem : AbstractSystem
    {
        protected override void OnInit()
        {
            AssetBundle asset = AssetBundle.LoadFromFile("");
            Sprite[] sprites = asset.LoadAllAssets<Sprite>();
        }
    }
}

