using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AssetBundleController : MonoBehaviour
{
    public static List<AssetBundle> bundles;

    public static AssetBundle GetBundleFromName(string name)
    {
        foreach (AssetBundle ab in bundles)
        {
            if (ab.name == name)
            {
                return ab;
            }
        }
        return null;
    }

    public static List<string> casenames;
    public static void GenerateBundlesList()
    {
        bundles = new List<AssetBundle>();
        casenames = new List<string>();
        string mainpath;
        if (Application.platform == RuntimePlatform.Android)
        {
            mainpath = Application.persistentDataPath;
        }
        else
        {
            mainpath = Application.streamingAssetsPath;
        }

        foreach (string dir in Directory.GetDirectories(mainpath))
        {
            if (!dir.Contains("Slot") && Path.GetFileName(dir) != "Unity")
            {
                AssetBundle ab = AssetBundle.LoadFromFile(Path.Combine(dir, Path.GetFileName(dir).ToLower()));
                bundles.Add(ab);
                casenames.Add(Path.GetFileName(dir));
            }
        }
    }

}
