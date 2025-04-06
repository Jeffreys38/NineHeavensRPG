using System;
using UnityEngine;

public class CutsceneEventListener : MonoBehaviour
{
      [Header("Listening To")] 
      [SerializeField] private CutsceneEventChannelSO _onCutSceneRequested;
      
      [Header("Broadcasting on")]
      [SerializeField] private LoadEventChannelSO _loadCutScene = default;

      public SaveSystem saveSystem;

      private void OnEnable()
      {
            _onCutSceneRequested.OnEventRaised += PlayCutsceneIfFirstTimeAsync;
      }

      private void OnDisable()
      {
            _onCutSceneRequested.OnEventRaised -= PlayCutsceneIfFirstTimeAsync;
      }
      
      private void PlayCutsceneIfFirstTimeAsync(CutsceneSO cutscene)
      {
            var finishedCutSceneGUIds = saveSystem.gameData.finishedCutSceneGUIds;
            
            if (!finishedCutSceneGUIds.Contains(cutscene.Guid))
            {
                  _loadCutScene.RaiseEvent(cutscene, showLoadingScreen: false, fadeScreen: true);
                  finishedCutSceneGUIds.Add(cutscene.Guid);
            }
      }
}