using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class FCServices0 : MonoBehaviour
{
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

    public static void TryUncombineMesh(GameObject test)
    {
        if (test.GetComponent<MeshCollider>() && test.GetComponent<MeshFilter>() && test.GetComponent<MeshFilter>().sharedMesh != test.GetComponent<MeshCollider>().sharedMesh)
        {
            test.GetComponent<MeshFilter>().sharedMesh = test.GetComponent<MeshCollider>().sharedMesh;
        }

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
}
