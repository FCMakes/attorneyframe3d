using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class FCServices : MonoBehaviour
{
    [System.Serializable]
    public class Interval
    {
        public float min;
        public float max;

        public bool Contains(float number)
        {
            if (number >= min && number <= max)
            {
                return true;
            }
            return false;
        }
        public bool isBigger(float number)
        {
            if (number > max)
            {
                return true;
            }
            return false;
        }
        public bool isSmaller(float number)
        {
            if (number < min)
            {
                return true;
            }
            return false;
        }
    }
    public static bool isMouseOverClickableUI()
    {
        foreach (Selectable b in GameObject.FindObjectsOfType<Selectable>())
        {
            if (b.gameObject.GetComponentInParent<Canvas>().enabled)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(b.gameObject.GetComponent<RectTransform>(), Input.mousePosition))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static Rect GetWorldSpaceRect(RectTransform rt)
    {
        var r = rt.rect;
        r.center = rt.TransformPoint(r.center);
        r.size = rt.TransformVector(r.size);
        return r;
    }
    public static void ButtonPressEffect(GameObject button)
    {
        Graphic graphic = button.GetComponent<Graphic>();
        graphic.CrossFadeColor(graphic.gameObject.GetComponent<Button>().colors.pressedColor, graphic.gameObject.GetComponent<Button>().colors.fadeDuration, true, true);
        button.GetComponent<Button>().onClick.Invoke();
    }
    public static void ButtonDownEffect(GameObject button)
    {
        Graphic graphic = button.GetComponent<Graphic>();
        graphic.CrossFadeColor(graphic.gameObject.GetComponent<Button>().colors.pressedColor, graphic.gameObject.GetComponent<Button>().colors.fadeDuration, true, true);

    }
    public static void ButtonUpEffect(GameObject button)
    {
        Graphic graphic = button.GetComponent<Graphic>();
        graphic.CrossFadeColor(graphic.gameObject.GetComponent<Button>().colors.normalColor, graphic.gameObject.GetComponent<Button>().colors.fadeDuration, true, true);
    }

    public static GameObject FindChildWithList(GameObject Parent, List<string> list)
    {
        GameObject result = Parent;
        foreach (string name in list)
        {
            result = FCServices.FindChildWithName(result, name);
        }
        return result;
    }

    public static GameObject FindRootGameObject(string name)
    {
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (go.name == name)
            {
                return go;
            }
        }
        return null;

    }

    public static void MakeListOfAllGameObjectsInScene(List<GameObject> toadd)
    {
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            toadd.Add(go);
            FCServices.MakeListOfAllDescendants(go, toadd);
        }
    }


    public static T[] GetAllObjectsOfTypeInScene<T>() where T : UnityEngine.Object
    {
        List<T> toreturn = new List<T>();
        List<GameObject> allgameobjects = new List<GameObject>();
        FCServices.MakeListOfAllGameObjectsInScene(allgameobjects);
        foreach (GameObject go in allgameobjects)
        {

            if (go.GetComponents<T>().Length > 0)
            {
                toreturn.AddRange(go.GetComponents<T>());
            }
        }
        return toreturn.ToArray();


    }

  public static GameObject GetGameObjectWithType(Type type)
    {
        List<GameObject> tocheck = new List<GameObject>();
        FCServices.MakeListOfAllGameObjectsInScene(tocheck);
        foreach (GameObject go in tocheck)
        {
            if (go.GetComponent(type))
            {
                return go;
            }
        }
        return null;
    }

    public static T GetComponentFromObjectOfType<T>(Type type) where T : UnityEngine.Object
    {
        return FCServices.GetGameObjectWithType(type).gameObject.GetComponent<T>();
    }

    public static T GetComponentFromGameObject<T>(string gobjname) where T: UnityEngine.Object
    {
        return GameObject.Find(gobjname).GetComponent<T>();
    }

    public static GameObject FindGameObjectWithHierarchy(string parentname, params string[] names)
    {
        GameObject result = null;
        GameObject newres = null;
        foreach (string name in names)
        {
            if (result == null)
            {
                result = FCServices.FindChildWithName(FCServices.FindRootGameObject(parentname), name);
            }
            else
            {
                newres = FCServices.FindChildWithName(result, name);
                result = newres;
            }


        }

        return newres;
    }


    public static GameObject FindChildWithArray(GameObject Parent, params string[] names)
    {
        GameObject result = null;
        GameObject newres = null;
        foreach (string name in names)
        {
            if (result == null)
            {
                result = FCServices.FindChildWithName(Parent, name);
            }
            else
            {
                newres = FCServices.FindChildWithName(result, name);
                result = newres;
            }


        }

        return newres;

    }
    public static GameObject FindChildWithArray(string parentname, params string[] names)
    {
        GameObject result = null;
        GameObject newres = null;
        foreach (string name in names)
        {
            if (result == null)
            {
                result = FCServices.FindChildWithName(parentname, name);
            }
            else
            {
                newres = FCServices.FindChildWithName(result, name);
                result = newres;
            }


        }

        return newres;

    }


    public static void CheckForCombinedMesh(GameObject test)
    {
        if (test.GetComponent<MeshCollider>() && test.GetComponent<MeshFilter>() && test.GetComponent<MeshFilter>().sharedMesh != test.GetComponent<MeshCollider>().sharedMesh)
        {
            test.GetComponent<MeshFilter>().sharedMesh = test.GetComponent<MeshCollider>().sharedMesh;
        }
        if (test.name == "Wall")
        {
            test.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Wall");
        }
        if (test.name == "Floor" || test.name == "Wood_Wall")
        {
            test.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Floor");
        }
        if (test.name == "Plank_Basic")
        {
            test.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Plank_Basic");
        }
        if (test.name == "Plank_WithCaps")
        {
            test.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Plank_WithCaps");
        }
        if (test.name == "StairCollider")
        {
            FCServices.FindChildWithName(test.transform.parent.gameObject, "Stairs02").GetComponent<MeshFilter>().sharedMesh = test.GetComponent<MeshCollider>().sharedMesh;
        }
        if (test.name == "Model_Tile_Ceiling_Wood")
        {
            test.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Tile_Ceiling_Wood");
        }
        if (test.name == "Model_Wall_Tile")
        {
            test.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Wall_Tile");
        }
        if (test.name == "MusicStand01")
        {
            test.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Stand");
            test.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (test.name == "CH3_Vent")
        {
            test.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("CH3_Vent");
        }
        if (test.name == "tile_wall_cement")
        {
            test.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("tile_wall_cement");
        }
        if (test.name == "Model_Tile_Wall_Office")
        {
            test.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("WallOffice");
        }
        if (test.name == "CH3_Floor_Ceramic")
        {
            test.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("CH3_Floor_Ceramic");
        }



    }

    public static void SetAnimatorBool(GameObject parent, string Name, bool Bool)
    {
        parent.GetComponent<Animator>().SetBool(Name, Bool);
    }

    public static void SetAnimatorTrigger(GameObject parent, string Name)
    {
        parent.GetComponent<Animator>().SetTrigger(Name);
    }
    public static void PlayAudio(GameObject parent)
    {
        parent.GetComponent<AudioSource>().Play();
    }

    public static void StopAudio(GameObject parent)
    {
        parent.GetComponent<AudioSource>().Stop();
    }

    public static Vector3 EulerToNiceRotation(Vector3 eulerAnglez)
    {
        Vector3 anglez = eulerAnglez;
        float angleX = anglez.x;
        angleX = (angleX > 180) ? angleX - 360 : angleX;
        float angleY = anglez.y;
        angleY = (angleY > 180) ? angleY - 360 : angleY;
        float angleZ = anglez.z;
        angleZ = (angleZ > 180) ? angleZ - 360 : angleZ;

        return new Vector3(angleX, angleY, angleZ);
    }
    public static Vector3 GetNiceRotation(GameObject TransformHolder)
    {

        Vector3 anglez = TransformHolder.transform.eulerAngles;
        float angleX = anglez.x;
        angleX = (angleX > 180) ? angleX - 360 : angleX;
        float angleY = anglez.y;
        angleY = (angleY > 180) ? angleY - 360 : angleY;
        float angleZ = anglez.z;
        angleZ = (angleZ > 180) ? angleZ - 360 : angleZ;

        return new Vector3(angleX, angleY, angleZ);


    }
    public static GameObject GetTarget()
    {

        GameObject crosshair = GameObject.FindWithTag("Crosshair");
        RaycastHit ray;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(crosshair.transform.position), out ray))
        {
            return ray.transform.gameObject;
        }
        return null;
    }
    public static GameObject GetMouseTarget()
    {


        RaycastHit ray;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray))
        {
            return ray.transform.gameObject;
        }
        return null;
    }
    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;
        return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
    }

    public static bool Compare(string tocompare, params string[] either)
    {
        foreach (string s in either)
        {
            if (tocompare == s)
            {
                return true;
            }
        }
        return false;
    }

    public static void ChangeLayerRecursive(GameObject parent, string LayerName)
    {
        parent.layer = LayerMask.NameToLayer(LayerName);
        foreach (Transform child in parent.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = LayerMask.NameToLayer(LayerName);
        }
    }
    public static GameObject GetPointTarget(Vector3 point)
    {


        RaycastHit ray;
        if (Physics.Raycast(GameObject.Find("TargetCam").gameObject.GetComponent<Camera>().ScreenPointToRay(point), out ray))
        {
            return ray.transform.gameObject;
        }
        return null;
    }
    public static GameObject GetMouseTarget(Camera touse)
    {


        RaycastHit ray;
        if (Physics.Raycast(touse.ScreenPointToRay(Input.mousePosition), out ray))
        {
            return ray.transform.gameObject;
        }
        return null;
    }

    public static bool isPosOverClickableUI(Vector3 v3)
    {
        foreach (Selectable b in GameObject.FindObjectsOfType<Selectable>())
        {
            if (b.gameObject.GetComponentInParent<Canvas>().enabled)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(b.gameObject.GetComponent<RectTransform>(), v3))
                {
                    return true;
                }
            }
        }

        return false;
    }
    public static void MakeListOfAllDescendants(GameObject parent, List<GameObject> toaddto)
    {
        foreach (Transform child in parent.transform)
        {
            toaddto.Add(child.gameObject);
            FCServices.MakeListOfAllDescendants(child.gameObject, toaddto);
        }

    }



    public static GameObject DeduceTarget(GameObject target)
    {
        if (target.name == "AiCollider")
        {
            if (target.transform.parent.gameObject.name.Contains("Tile_Wall") && FCServices.FindChildWithName(target.transform.parent.gameObject, "Wall"))
            {
                return FCServices.FindChildWithName(target.transform.parent.gameObject, "Wall");
            }
            if (target.transform.parent.gameObject.name.Contains("Tile_Wall_Wood"))
            {
                return FCServices.FindChildWithName(target.transform.parent.gameObject, "Wood_Wall");
            }
            if (target.transform.parent.gameObject.name.Contains("Prop_WoodBarrel_Static"))
            {
                target.transform.SetParent(FCServices.FindChildWithName(target.transform.parent.gameObject, "WoodBarrel01").transform);
                return target.transform.parent.gameObject;
            }
            if (target.transform.parent.gameObject.name.Contains("Tile_Wall_Tile"))
            {
                return FCServices.FindChildWithName(target.transform.parent.gameObject, "Model_Wall_Tile");
            }
            if (target.transform.parent.gameObject.name.Contains("MusicStand"))
            {
                target.transform.SetParent(FCServices.FindChildWithName(target.transform.parent.gameObject, "MusicStand01").transform);
                return target.transform.parent.gameObject;
            }
            if (target.transform.parent.gameObject.name.Contains("Tile_Wall_Cement"))
            {
                return FCServices.FindChildWithName(target.transform.parent.gameObject, "tile_wall_cement");
            }
            return target.transform.parent.gameObject;
        }
        if (target.name == "StairCollider")
        {
            FCServices.FindChildWithName(target.transform.parent.gameObject, "Stairs02").AddComponent<MeshCollider>().sharedMesh = target.GetComponent<MeshCollider>().sharedMesh;
            Destroy(target);

            return FCServices.FindChildWithName(target.transform.parent.gameObject, "Stairs02");
        }
        if (target.name == "Collider" && FCServices.FindChildWithName(target.transform.parent.gameObject, "PipeContainer"))
        {
            GameObject pipec = FCServices.FindChildWithName(target.transform.parent.gameObject, "PipeContainer");
            FCServices.FindChildWithName(pipec, "Pipe").AddComponent<BoxCollider>().size = target.GetComponent<BoxCollider>().size;
            Destroy(target);

            return FCServices.FindChildWithName(pipec, "Pipe");
        }
        if (target.name == "Back" || target.name == "Front")
        {
            if (FCServices.FindChildWithName(target.transform.parent.gameObject, "Model_Basic_Door_01"))
            {
                return FCServices.FindChildWithName(target.transform.parent.gameObject, "Model_Basic_Door_01");
            }
            if (FCServices.FindChildWithName(target.transform.parent.gameObject, "Model_Basic_Door_02"))
            {
                return FCServices.FindChildWithName(target.transform.parent.gameObject, "Model_Basic_Door_02");
            }
            if (FCServices.FindChildWithName(target.transform.parent.gameObject, "Model_Door_Office"))
            {
                FCServices.FindChildWithName(target.transform.parent.gameObject, "Model_Door_Office").AddComponent<MeshCollider>();
                return FCServices.FindChildWithName(target.transform.parent.gameObject, "Model_Door_Office");
            }
        }

        return target;
    }

    public static IEnumerator DelayedAction(Action todelay, float time)
    {
        yield return new WaitForSeconds(time);
        {
            todelay.Invoke();
        }
    }

    public static IEnumerator DoNextFrame(Action todelay)
    {
        yield return new WaitForEndOfFrame();
        {
            todelay.Invoke();
        }
    }

    public static bool anyKeyUp()
    {
        foreach (KeyCode kc in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyUp(kc) && kc != KeyCode.None)
            {
                return true;
            }
        }
        return false;

    }

    public static GameObject FindChildWithName(GameObject Parent, string Name)
    {
        foreach (Transform child in Parent.transform)
        {
            if (child.gameObject.name == Name)
            {
                return child.gameObject;
            }


        }
        return null;
    }

    public static string NumToString2Symbls(int num)
    {
        if (num < 10)
        {
            return "0" + num.ToString();
        }
        else
        {
            return num.ToString();
        }
    }
    public static GameObject FindChildWithName(string parentname, string Name)
    {
        foreach (Transform child in GameObject.Find(parentname).transform)
        {
            if (child.gameObject.name == Name)
            {
                return child.gameObject;
            }


        }
        return null;
    }

    public static UnityEngine.Object ObjectFromArrayByName(string name, UnityEngine.Object[] tofind)
    {
        foreach (UnityEngine.Object ob in tofind)
        {
            if (ob.name == name)
            {
                return ob;
            }
        }
        return null;
    }

#if UNITY_EDITOR
    public static UnityEditor.Animations.AnimatorState GetStateByName(string name, AnimatorController tofind, int layerindex)
    {
        foreach (ChildAnimatorState AnS in tofind.layers[layerindex].stateMachine.states)
        {
            if (AnS.state.name == name)
            {
                return AnS.state;
            }
        }
        return null;

    }
#endif

    public static float SmartParse(string text)
    {
        FCServices.ReplaceCommaWithPoint(text);
        if (text.Contains(","))
        {
            return float.Parse(text);
        }
        if (text.Contains("."))
        {
            return float.Parse(text, CultureInfo.InvariantCulture);
        }
        if (!text.Contains(",") || !text.Contains("."))
        {
            return float.Parse(text);
        }
        return 0f;

    }

    public static string ReplaceCommaWithPoint(string text)
    {
        return text.Replace(",", ".");
    }

    public static void AllAnimatorBoolsToFalse(Animator controller)
    {
        AnimatorControllerParameter[] parameters = controller.parameters;
        foreach (AnimatorControllerParameter param in parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                controller.SetBool(param.name, false);
            }
        }
    }

    public static void AllAnimatorBoolsToFalse(Animator controller, params string[] ignore)
    {
        AnimatorControllerParameter[] parameters = controller.parameters;
        foreach (AnimatorControllerParameter param in parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                if (Array.IndexOf(ignore, param.name) == -1)
                {
                    controller.SetBool(param.name, false);
                }
            }
        }
    }
}
