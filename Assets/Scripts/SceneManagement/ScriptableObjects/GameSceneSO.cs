using UnityEngine.AddressableAssets;
using UnityEngine.Localization;

/// <summary>
/// This class is a base class which contains what is common to all game scenes (Locations, Menus, Managers)
/// </summary>
public class GameSceneSO : DescriptionBaseSO
{
    public GameSceneType sceneType;
    public AssetReference sceneReference; // Used at runtime to load the scene from the right AssetBundle
    public AudioCueSO musicTrack;
    public CutsceneSO cutscene;

    /// <summary>
    /// Used by the SceneSelector tool to discern what type of scene it needs to load
    /// </summary>
    public enum GameSceneType
    {
        // Playable scenes
        Location,
        Menu,

        // Special scenes
        Initialisation,
        PersistentManagers,
        Gameplay,

        // Work in progress scenes that don't need to be played
        Art,
        Cutscene
    }
}