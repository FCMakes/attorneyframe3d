using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CaseSelectBTN : FCMakes.LazyPort.ButtonInputManager
{

    public void Update()
    {
        if (GetKeyDown)
        {
            base.gameObject.GetComponent<AudioSource>().Play();
            base.StartCoroutine(this.LoadSceneFromBundle());
          
        }
    }

    public IEnumerator LoadSceneFromBundle()
    {
        yield return new WaitForSeconds(1f);
        if (Directory.Exists(Path.Combine(Application.persistentDataPath, "Slot0")))
        {
            Directory.Delete(Path.Combine(Application.persistentDataPath, "Slot0"), true);
        }
        Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("FadeEffect4"));
        yield return new WaitForSeconds(1.5f);
        AssetBundle toloadfrom;
        toloadfrom = AssetBundleController.GetBundleFromName(base.gameObject.GetComponentInChildren<Text>().text.ToLower());
        string[] scenePath = toloadfrom.GetAllScenePaths();
        Debug.Log(scenePath[0]);
        UnityEngine.SceneManagement.SceneManager.LoadScene(scenePath[0]);
    }
}
