#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AssetBundleCreator : MonoBehaviour
{
    [MenuItem("Asset Bundles/Build All Bundles")]
    static void BuildBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/PC", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
	[MenuItem("Asset Bundles/Build All PC Bundles")]
	static void BuildBundlesPC()
	{
		BuildPipeline.BuildAssetBundles("Assets/AssetBundles/PC", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
		
	}
	[MenuItem("Asset Bundles/Build All Android Bundles")]
	static void BuildBundlesAndroid()
	{
		BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);

	}
	[MenuItem("Asset Bundles/Build Bundle from Selection")]
	static void FromObj()
	{
		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		string name = AssetDatabase.GetImplicitAssetBundleName(path);
		AssetBundleCreator.BuildAssetBundlesByName(name);
		AssetBundleCreator.BuildAssetBundlesByNameAndroid(name);
	}

	[MenuItem("Asset Bundles/Build PC Bundle from Selection")]
	static void PCFromObj()
	{
		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		string name = AssetDatabase.GetImplicitAssetBundleName(path);
		AssetBundleCreator.BuildAssetBundlesByName(name);
	}

	[MenuItem("Asset Bundles/Build Android Bundle from Selection")]
	static void AndroidFromObj()
	{
		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		string name = AssetDatabase.GetImplicitAssetBundleName(path);
		AssetBundleCreator.BuildAssetBundlesByNameAndroid(name);
	}

	public static void BuildAssetBundlesByName(params string[] assetBundleNames)
	{
		// Argument validation
		if (assetBundleNames == null || assetBundleNames.Length == 0)
		{
			return;
		}

		// Remove duplicates from the input set of asset bundle names to build.
		assetBundleNames = assetBundleNames.Distinct().ToArray();

		List<AssetBundleBuild> builds = new List<AssetBundleBuild>();

		foreach (var assetBundle in assetBundleNames)
		{
			var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundle);

			AssetBundleBuild build = new AssetBundleBuild();
			build.assetBundleName = assetBundle;
			build.assetNames = assetPaths;

			builds.Add(build);
		}

		// TODO: set the desired BuildAssetBundleOptions
		BuildPipeline.BuildAssetBundles("Assets/AssetBundles/PC", builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
	}
	public static void BuildAssetBundlesByNameAndroid(params string[] assetBundleNames)
	{
		// Argument validation
		if (assetBundleNames == null || assetBundleNames.Length == 0)
		{
			return;
		}

		// Remove duplicates from the input set of asset bundle names to build.
		assetBundleNames = assetBundleNames.Distinct().ToArray();

		List<AssetBundleBuild> builds = new List<AssetBundleBuild>();

		foreach (var assetBundle in assetBundleNames)
		{
			var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundle);

			AssetBundleBuild build = new AssetBundleBuild();
			build.assetBundleName = assetBundle;
			build.assetNames = assetPaths;

			builds.Add(build);
		}

		// TODO: set the desired BuildAssetBundleOptions
		BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Android", builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.Android);
	}
}
#endif