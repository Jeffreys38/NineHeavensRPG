using UnityEngine;

/// <summary>
/// This class contains Settings specific to Cutscene only
/// </summary>

[CreateAssetMenu(fileName = "NewCutscene", menuName = "Scene Data/Cutscene")]
public class CutsceneSO : GameSceneSO
{
    public bool isViewed = false;
}