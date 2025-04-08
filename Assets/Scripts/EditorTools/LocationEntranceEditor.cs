#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocationEntrance))]
public class LocationEntranceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LocationEntrance entrance = (LocationEntrance)target;

        if (GUILayout.Button("Create Spawn Point Override"))
        {
            GameObject spawnPoint = new GameObject("SpawnPointOverride");
            spawnPoint.transform.SetParent(entrance.transform);
            spawnPoint.transform.localPosition = new Vector3(0, 0, 1.5f);
            entrance.spawnPointOverride = spawnPoint.transform;

            EditorUtility.SetDirty(entrance);
        }
    }
}
#endif