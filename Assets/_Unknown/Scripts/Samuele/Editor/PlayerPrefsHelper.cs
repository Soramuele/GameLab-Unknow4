using UnityEditor;
using UnityEngine;

public class PlayerPrefsHelper : EditorWindow
{
    [MenuItem("Tools/PlayerPrefs/Clear All")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
