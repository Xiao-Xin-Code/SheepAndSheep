#if UNITY_EDITOR

using System.IO;
using UnityEngine;

public class AssetCreate : MonoBehaviour
{
    bool ison = false;
    void Start()
	{
        if (ison)
        {
			GameConfig config = new GameConfig
			{
				musicIson = true,
				sfxIson = true,
				shakeIson = true,
				theme = "Default",
				skipFirstLevel = false
			};

			string json = JsonUtility.ToJson(config);
			File.WriteAllText(Application.streamingAssetsPath + "/Config/GameConfig", json);
		}
	}
}

#endif