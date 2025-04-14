using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(SkillSO))]
public class CopyGuidEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SerializableScriptableObject o = (SerializableScriptableObject)target;
       
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("GUID", o.Guid);

        if (GUILayout.Button("Copy GUID"))
        {
            EditorGUIUtility.systemCopyBuffer = o.Guid;
            Debug.Log($"Copied GUID: {o.Guid}");
        }
    }
}
#endif
