// using UnityEngine;
// #if UNITY_EDITOR
// using UnityEditor;
// using UnityEditor.AddressableAssets;
// using UnityEditor.AddressableAssets.Settings;
// #endif
// using UnityEngine.AddressableAssets;
//
// public class SerializableScriptableObject : ScriptableObject
// {
//     [SerializeField] private string _guid;
//     public string Guid => _guid;
//
//     protected virtual void OnEnable()
//     {
// #if UNITY_EDITOR
//         UpdateGuidInEditor();
// #else
//         UpdateGuidInRuntime();
// #endif
//     }
//
// #if UNITY_EDITOR
//     private void UpdateGuidInEditor()
//     {
//         string path = AssetDatabase.GetAssetPath(this);
//         if (!string.IsNullOrEmpty(path))
//         {
//             string guid = AssetDatabase.AssetPathToGUID(path);
//             if (_guid != guid)
//             {
//                 _guid = guid;
//                 EditorUtility.SetDirty(this);
//             }
//         }
//     }
// #endif
//
//     private void UpdateGuidInRuntime()
//     {
// #if !UNITY_EDITOR
//         if (string.IsNullOrEmpty(_guid))
//         {
//             var location = Addressables.RuntimePath;
//             _guid = this.name;
//         }
// #endif
//     }
// }

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SerializableScriptableObject : ScriptableObject
{
    [SerializeField, HideInInspector] private string _guid;
    public string Guid => _guid;

#if UNITY_EDITOR
    void OnValidate()
    {
        var path = AssetDatabase.GetAssetPath(this);
        _guid = AssetDatabase.AssetPathToGUID(path);
    }
#endif
}
