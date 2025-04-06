using System;
using UnityEngine;

public class CutsceneEventListener : MonoBehaviour
{
      [Header("Listening To")] 
      [SerializeField] private CutsceneEventChannelSO _onCutSceneRequested;
      
      [Header("Broadcasting on")]
      [SerializeField] private LoadEventChannelSO _loadLocation = default;

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
            
      }
}