using UnityEngine;

public class CutsceneEventListener : MonoBehaviour
{
      [Header("Listening To")] 
      [SerializeField] private CutsceneEventChannelSO _onCutSceneRequested;
      
      [Header("Broadcasting on")]
      [SerializeField] private LoadEventChannelSO _loadScene;

      private CutsceneSO _currentCutscene;
      public SaveSystem saveSystem;

      private void OnEnable()
      {
            _onCutSceneRequested.OnEventRaised += PlayCutsceneIfFirstTimeAsync;
      }

      private void OnDisable()
      {
            _onCutSceneRequested.OnEventRaised -= PlayCutsceneIfFirstTimeAsync;
      }
      
      private void PlayCutsceneIfFirstTimeAsync(GameSceneSO gameScene)
      {
            if (gameScene == null) return;
            
            var finishedCutSceneGUIds = saveSystem.gameData.finishedCutSceneGUIds;
            if (gameScene.cutscene != null && !finishedCutSceneGUIds.Contains(gameScene.cutscene.Guid))
            {
                  _loadScene.RaiseEvent(gameScene.cutscene, showLoadingScreen: false, fadeScreen: true);
                  finishedCutSceneGUIds.Add(gameScene.cutscene.Guid);
            }
            else
            {
                  _loadScene.RaiseEvent(gameScene, showLoadingScreen: false, fadeScreen: true);
            }
      }
}