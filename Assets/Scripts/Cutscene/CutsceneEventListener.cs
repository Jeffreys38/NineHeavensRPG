using UnityEngine;

public class CutsceneEventListener : MonoBehaviour
{
      [Header("Listening To")] 
      [SerializeField] private CutsceneEventChannelSO _onCutSceneRequested;
      
      [Header("Broadcasting on")]
      [SerializeField] private LoadEventChannelSO _loadLocation;
      [SerializeField] private LoadEventChannelSO _loadMenu;

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
                  _loadMenu.RaiseEvent(gameScene.cutscene, showLoadingScreen: false, fadeScreen: false);
                  finishedCutSceneGUIds.Add(gameScene.cutscene.Guid);
            }
            else
            {
                  _loadLocation.RaiseEvent(gameScene, showLoadingScreen: false, fadeScreen: true);
            }
      }
}