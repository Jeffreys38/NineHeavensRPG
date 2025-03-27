using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressableLoader
{
    public static void LoadAssetsByGuids<T>(List<string> guids, Action<List<T>> onComplete)
    {
        CoroutineRunner.instance.StartCoroutine(LoadAssetsCoroutine(guids, onComplete));
    }

    private static IEnumerator LoadAssetsCoroutine<T>(List<string> guids, Action<List<T>> onComplete)
    {
        List<T> loadedAssets = new List<T>();

        foreach (var guid in guids)
        {
            var handle = Addressables.LoadAssetAsync<T>(guid);
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                loadedAssets.Add(handle.Result);
            }
            else
            {
                Debug.LogError($"Failed to load asset with GUID: {guid}");
            }
        }

        onComplete?.Invoke(loadedAssets);
    }
}