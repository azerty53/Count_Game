using UnityEngine;
using System.Collections;
using UnityEditor;
public class CreateScriptableObject : MonoBehaviour {

    [MenuItem("Assets/Create/My Scriptable Object")]
    public static void CreateMyAsset()
    {
        MyScriptableObject asset = ScriptableObject.CreateInstance<MyScriptableObject>();
        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/NewScriptableObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

}
