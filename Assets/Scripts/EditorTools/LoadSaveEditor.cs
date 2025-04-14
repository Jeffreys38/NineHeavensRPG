using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class LoadSaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager)target;

        if (GUILayout.Button("Save data to disk"))
        {
            gameManager.saveSystem.SaveDataToDisk();
        }
    }
}
#endif