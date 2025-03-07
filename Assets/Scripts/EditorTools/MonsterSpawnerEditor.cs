using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(MonsterSpawner))]
public class MonsterSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MonsterSpawner spawner = (MonsterSpawner)target;

        if (GUILayout.Button("Update Spawn Positions"))
        {
            if (spawner.SpawnerData != null)
            {
                spawner.SpawnerData.UpdateSpawnPositions(spawner.transform);
                EditorUtility.SetDirty(spawner.SpawnerData);
            }
            else
            {
                Debug.LogWarning("No MonsterSpawnerDataSO assigned!");
            }
        }
    }
}
#endif