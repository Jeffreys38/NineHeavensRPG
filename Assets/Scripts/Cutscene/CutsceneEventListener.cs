using System.Collections.Generic;
using UnityEngine;

public class CutsceneEventListener : MonoBehaviour
{
      [Header("Listening To")] 
      [SerializeField] private CutsceneEventChannelSO _onCutSceneRequested;
      [SerializeField] private VoidEventChannelSO _onSkipCutsceneRequested;
      
      [Header("Broadcasting on")]
      [SerializeField] private LoadEventChannelSO _loadLocation;
      [SerializeField] private LoadEventChannelSO _loadMenu;
      
      private GameSceneSO _currentScene;
      private List<string> _finishedCutSceneGUIds;
      
      public SaveSystem saveSystem;

      private void OnEnable()
      {
            _onCutSceneRequested.OnEventRaised += PlayCutsceneIfFirstTimeAsync;
            _onSkipCutsceneRequested.OnEventRaised += SkipCutscene;
      }

      private void OnDisable()
      {
            _onCutSceneRequested.OnEventRaised -= PlayCutsceneIfFirstTimeAsync;
            _onSkipCutsceneRequested.OnEventRaised -= SkipCutscene;
      }
      
      private void PlayCutsceneIfFirstTimeAsync(GameSceneSO gameScene)
      {
            _currentScene = gameScene;
            _finishedCutSceneGUIds = saveSystem.gameData._finishedCutSceneGUIds;
            if (_currentScene.cutscene && !_finishedCutSceneGUIds.Contains(gameScene.cutscene.Guid))
            {
                  _loadMenu.RaiseEvent(gameScene.cutscene, showLoadingScreen: false, fadeScreen: false);
                  
                  var guid = _currentScene.cutscene.Guid;
                  MarkCutsceneAsFinished(guid);
            }
            else
            {
                  _loadLocation.RaiseEvent(gameScene, showLoadingScreen: false, fadeScreen: true);
            }
      }

      private void SkipCutscene()
      {
            var guid = _currentScene.cutscene.Guid;
            MarkCutsceneAsFinished(guid);
            
            _loadLocation.RaiseEvent(_currentScene, showLoadingScreen: false, fadeScreen: true);
      }
      
      private void MarkCutsceneAsFinished(string cutsceneGuid)
      {
            if (!_finishedCutSceneGUIds.Contains(cutsceneGuid))
            {
                  _finishedCutSceneGUIds.Add(cutsceneGuid);
                  saveSystem.gameData._finishedCutSceneGUIds = _finishedCutSceneGUIds;
            }
      }
}