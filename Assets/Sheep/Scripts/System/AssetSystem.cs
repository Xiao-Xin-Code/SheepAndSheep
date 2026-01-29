using DG.Tweening.Plugins.Core.PathCore;
using QMVC;
using System.Collections.Generic;
using System.IO;
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

		private Dictionary<string, AudioClip> bgms;
		private Dictionary<string, AudioClip> sfxs;

		TextAsset[] assets;

        DataModel _dataModel;

		public Sprite BgSprite { get => bgSprite; }
		public Sprite MaskSprite { get => maskSprite; }

		protected override void OnInit()
        {
			_dataModel = this.GetModel<DataModel>();

			_dataModel.Theme.RegisterWithInitValue(OnThemeChanged);

			sfx = Resources.Load<SFXController>("SFX");
            block = Resources.Load<BlockController>("Item");
			

			if(Application.platform == RuntimePlatform.Android)
			{
                TextAsset[] tempAssets = Resources.LoadAll<TextAsset>("Level");
				TextAsset defaultAsset = Resources.Load<TextAsset>("Default/level_0");
				assets = new TextAsset[tempAssets.Length + 1];

				assets[0] = defaultAsset;
				for(int i = 0; i < tempAssets.Length; i++)
				{
					assets[i + 1] = tempAssets[i];
				}

                LoadAllAudioClip();

				TextAsset configText = Resources.Load<TextAsset>("Config/GameConfig");

				if (configText == null) Application.Quit();

                GameConfig config = JsonUtility.FromJson<GameConfig>(configText?.text);
                _dataModel.MusicIsOn.Value = config.musicIson;
                _dataModel.SfxIsOn.Value = config.sfxIson;
                _dataModel.ShakeIsOn.Value = config.shakeIson;
                _dataModel.Theme.Value = config.theme;
                _dataModel.skipFirstLevel = config.skipFirstLevel;

            }
			else
			{
                string levelPath = Application.streamingAssetsPath + "/Level";
                _dataModel.levelPaths = Directory.GetFiles(levelPath, "*.txt");

                LoadAllAudioClip();

                GameConfig config = JsonUtility.FromJson<GameConfig>(File.ReadAllText(Application.streamingAssetsPath + "/Config/GameConfig"));
                _dataModel.MusicIsOn.Value = config.musicIson;
                _dataModel.SfxIsOn.Value = config.sfxIson;
                _dataModel.ShakeIsOn.Value = config.shakeIson;
                _dataModel.Theme.Value = config.theme;
                _dataModel.skipFirstLevel = config.skipFirstLevel;
            }
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
            if (Application.platform == RuntimePlatform.Android)
			{
                TextAsset asset = assets[0];
                if (asset == null) return new string[0];

                return asset.text.Split(
                    new[] { '\n', '\r' },
                    System.StringSplitOptions.RemoveEmptyEntries
                );
            }
			else
			{
                string[] lines = File.ReadAllLines(_dataModel.levelPaths[0]);
                return lines;
            }
		}

		public string[] GetLevel()
		{
            if (Application.platform == RuntimePlatform.Android)
            {
				int index = Random.Range(1, assets.Length);
                TextAsset asset = assets[index];
                if (asset == null) return new string[0];

                return asset.text.Split(
                    new[] { '\n', '\r' },
                    System.StringSplitOptions.RemoveEmptyEntries
                );
            }
			else
			{
                int index = Random.Range(1, _dataModel.levelPaths.Length);
                string[] lines = File.ReadAllLines(_dataModel.levelPaths[index]);
                return lines;
            }
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

		
		public AudioClip GetBGM(string clip)
		{
			if (bgms.ContainsKey(clip))
			{
				return bgms[clip];
			}
			return null;
		}

		public AudioClip GetSFX(string clip)
		{
			if (sfxs.ContainsKey(clip))
			{
				return sfxs[clip];
			}
			return null;
		}



		private void LoadAllAudioClip()
		{
			this.bgms = new Dictionary<string, AudioClip>();
			this.sfxs = new Dictionary<string, AudioClip>();
			AudioClip[] bgms = Resources.LoadAll<AudioClip>($"Audio/BGM");
			AudioClip[] sfxs = Resources.LoadAll<AudioClip>($"Audio/SFX");
			foreach (var item in bgms)
			{
				this.bgms.Add(item.name, item);
			}
			foreach (var item in sfxs)
			{
				this.sfxs.Add(item.name, item);
			}
		}

	}


    

}

public static class ResourcesExtensions
{
    // 扩展方法：直接加载string
    public static string LoadText(this Resources _, string path)
    {
        TextAsset asset = Resources.Load<TextAsset>(path);
        return asset?.text;
    }

    // 扩展方法：直接加载string数组（行）
    public static string[] LoadLines(this Resources _, string path)
    {
        TextAsset asset = Resources.Load<TextAsset>(path);
        if (asset == null) return new string[0];

        return asset.text.Split(
            new[] { '\n', '\r' },
            System.StringSplitOptions.RemoveEmptyEntries
        );
    }

    // 扩展方法：直接加载所有文本资源
    public static string[] LoadAllTexts(this Resources _, string folderPath)
    {
        TextAsset[] assets = Resources.LoadAll<TextAsset>(folderPath);
        string[] texts = new string[assets.Length];

        for (int i = 0; i < assets.Length; i++)
        {
            texts[i] = assets[i].text;
        }

        return texts;
    }
}


