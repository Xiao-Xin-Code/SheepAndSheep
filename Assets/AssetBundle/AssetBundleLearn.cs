#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetBundleLearn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       

        List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();

        for(int i = 0; i < 10; i++)
        {
			AssetBundleBuild build = new AssetBundleBuild();
			build.assetBundleName = "";
			build.assetBundleVariant = "";

            assetBundleBuilds.Add(build);
		}



        BuildPipeline.BuildAssetBundles("", assetBundleBuilds.ToArray(), BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
#endif