using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(TW_MultiStrings_Regular)), CanEditMultipleObjects]
[Serializable]
public class TW_MultiStrings_Regular_Editor : Editor
{
    private int indexOfString;
    private static string[] PointerSymbols = { "None", "<", "_", "|", ">" };
    private TW_MultiStrings_Regular TW_MS_RegularScript;


    private void Awake() {
        TW_MS_RegularScript = (TW_MultiStrings_Regular)target;
    }

    private void MakeArrayGUI(SerializedObject obj, string name)
    {
        int size = obj.FindProperty(name + ".Array.size").intValue;
        int newSize = size;
        if (newSize != size)
            obj.FindProperty(name + ".Array.size").intValue = newSize;
        int[] array_value = new int[newSize];
        for (int i = 1; i < newSize; i++)
        {
            array_value[i] = i;
        }
        string[] array_content = new string[newSize];
        for (int i = 1; i < newSize; i++)
        {
            array_content[i] = (array_value[i] + 1).ToString();
        }
        if (TW_MS_RegularScript.MultiStrings.Count == 0)
            EditorGUILayout.HelpBox("Number of Strings must be more than 0!", MessageType.Error);
        MakePopup(obj);
        EditorGUILayout.HelpBox("Chose number of string in PoPup and edit text in TextArea below", MessageType.Info, true);
        indexOfString = EditorGUILayout.IntPopup("Edit string №", indexOfString, array_content, array_value, EditorStyles.popup);
        TW_MS_RegularScript.MultiStrings[indexOfString] = EditorGUILayout.TextArea(TW_MS_RegularScript.MultiStrings[indexOfString], GUILayout.ExpandHeight(true));
    }

    private void MakePopup(SerializedObject obj)
    {
        TW_MS_RegularScript.pointer = EditorGUILayout.Popup("Pointer symbol", TW_MS_RegularScript.pointer, PointerSymbols, EditorStyles.popup);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SerializedObject SO = new SerializedObject(TW_MS_RegularScript);
        MakeArrayGUI(SO, "MultiStrings");
    }
}
#endif

public class TW_MultiStrings_Regular : MonoBehaviour {

    public bool LaunchOnStart = true;
    public int timeOut = 1;
    public List<string> MultiStrings;
    public AudioSource src;
    public bool silent;
    public bool isTyping;
    public float speed;
   
    public int pointer=0;
    public string ORIGINAL_TEXT;

    private float time = 0f;
    public int сharIndex = 0;
    private int index_of_string = 0;
   public bool start;
    private List<int> n_l_list;
    private static string[] PointerSymbols = { "None", "<", "_", "|", ">" };
    public float fois;
    void Start ()
    {
        MultiStrings[0] = gameObject.GetComponent<Text>().text;
        ORIGINAL_TEXT = gameObject.GetComponent<Text>().text;
        gameObject.GetComponent<Text>().text = "";
        if (LaunchOnStart)
        {
            StartTypewriter();
        }
    }
	
	void Update () {
        if (start == true){
            NewLineCheck(ORIGINAL_TEXT);
        }
        if (MultiStrings.Contains(gameObject.GetComponent<Text>().text) && MultiStrings[MultiStrings.Count -1] == gameObject.GetComponent<Text>().text || GameObject.FindObjectOfType<ModifyStrings>().canInvestigate || GameObject.FindObjectOfType<ModifyStrings>().isExamining3d)
        {
            this.isTyping = false;
        }
        else
        {
            this.isTyping = true;
        }
    }

    public void StartTypewriter()
    {
        start = true;
        сharIndex = 0;
        time = 0f;
        fois = 1f;
    }

    public void SkipTypewriter()
    {
        
        сharIndex = ORIGINAL_TEXT.Length - 1;

    }

    public void NextString()
    {
        fois = 1f;
        start = true;
        сharIndex = 0;
        time = 0f;
        if (index_of_string + 1 < MultiStrings.Count){
            index_of_string++;
        }
        else{
            index_of_string = 0;
        }
        ORIGINAL_TEXT = MultiStrings[index_of_string];
    }

    public void LastString()
    {
        start = true;
       
        сharIndex = ORIGINAL_TEXT.Length - 1;
    }

    private void NewLineCheck(string S)
    {
        if (S.Contains(@"\"))
        {
            int multistringsid = MultiStrings.IndexOf(ORIGINAL_TEXT);
            ORIGINAL_TEXT = ORIGINAL_TEXT.Split(char.Parse(@"\"))[0] + Environment.NewLine + ORIGINAL_TEXT.Split(char.Parse(@"\"))[1];
            MultiStrings[multistringsid] = ORIGINAL_TEXT;
            StartCoroutine(MakeTypewriterTextWithNewLine(S, GetPointerSymbol(), MakeList(S)));
        }
        else
        {
            StartCoroutine(MakeTypewriterText(S, GetPointerSymbol()));
        }
    }

    private IEnumerator MakeTypewriterText(string ORIGINAL, string POINTER)
    {
        start = false;
        if (сharIndex != ORIGINAL.Length + 1)
        {
            
            string emptyString = new string(' ', ORIGINAL.Length-POINTER.Length);
            string TEXT = ORIGINAL.Substring(0, сharIndex);

          
            if (ORIGINAL.ToCharArray().Length > this.сharIndex && ORIGINAL.ToCharArray()[this.сharIndex].ToString() == "<")
            {
                
                TEXT = ORIGINAL.Substring(0, сharIndex + 24);
                
                  
                
            }

           
            if (сharIndex < ORIGINAL.Length) TEXT = TEXT + POINTER + emptyString.Substring(сharIndex);
            gameObject.GetComponent<Text>().text = TEXT;

            time += 1;
             
          
            yield return new WaitForSeconds(Time.fixedDeltaTime * (1 / speed));
            if (fois == 1)
            {
                if (!src.isPlaying && gameObject.GetComponent<Text>().text != "" && !silent && ORIGINAL.ToCharArray().Length > this.сharIndex && ORIGINAL.ToCharArray()[this.сharIndex].ToString() != " ")
                {

                    src.Play();
                }
                if (GameObject.FindObjectOfType<ModifyStrings>().lines.Length > 9 && GameObject.FindObjectOfType<ModifyStrings>().lines[9] != "")
                {
                    if (this.сharIndex == int.Parse(GameObject.FindObjectOfType<ModifyStrings>().lines[9]))
                    {

                        GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<SEController>().FindSE(GameObject.FindObjectOfType<ModifyStrings>().lines[10]);
                        if (!GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().isPlaying)
                        {
                            GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().Play();
                        }


                        Instantiate(PrefabController.Instance().FindPrefab("Flash"));

                        if (GameObject.FindObjectOfType<ModifyStrings>().lines[10] != "ayashii" && !GameObject.FindObjectOfType<ModifyStrings>().lines[10].Contains("diminish"))
                        {
                            GameObject.FindObjectOfType<ModifyStrings>().RumbleController(15);
                            GameObject.FindObjectOfType<ModifyStrings>().ShakeCamera();
                        }



                    }

                }
            }
            CharIndexPlus();
            start = true;

            if (fois == 1)
            {
                fois = 2;
            }
            else
            {
                fois = 1;
            }
        }
    }

    private IEnumerator MakeTypewriterTextWithNewLine(string ORIGINAL, string POINTER, List<int> List)
    {
        start = false;

        if (сharIndex != ORIGINAL.Length + 1)
        {
            this.isTyping = true;
            string emptyString = new string(' ', ORIGINAL.Length - POINTER.Length);
            string TEXT = ORIGINAL.Substring(0, сharIndex);
            if (сharIndex < ORIGINAL.Length) TEXT = TEXT + POINTER + emptyString.Substring(сharIndex);
            TEXT = InsertNewLine(TEXT, List);
            gameObject.GetComponent<Text>().text = TEXT;
            time += 1f;
            yield return new WaitForSeconds(Time.fixedDeltaTime * (1 / speed));
            CharIndexPlus();
            start = true;
        }
        else
        {
            this.isTyping = false;
        }
    }

    private List<int> MakeList(string S)
    {
        n_l_list = new List<int>();
        for (int i = 0; i < S.Length; i++)
        {
            if (S[i] == '\n')
            {
                n_l_list.Add(i);
            }
        }
        return n_l_list;
    }

    private string InsertNewLine(string _TEXT, List<int> _List)
    {
        for (int index = 0; index < _List.Count; index++)
        {
            if (сharIndex - 1 < _List[index])
            {
                _TEXT = _TEXT.Insert(_List[index], "\n");
            }
        }
        return _TEXT;
    }

    private string GetPointerSymbol()
    {
        if (pointer == 0){
            return "";
        }
        else{
            return PointerSymbols[pointer];
        }
    }

    private void CharIndexPlus()
    {
        if (time == timeOut)
        {
            time = 0f;
            if (ORIGINAL_TEXT.ToCharArray().Length > this.сharIndex && ORIGINAL_TEXT.ToCharArray()[this.сharIndex].ToString() == "<")
            {
                this.сharIndex += 23;
            }
                сharIndex += 1;
        }
    }
}


