using UnityEngine;

public class LocalDataHandler : IDataHandler
{
    public GameData Load()
    {
        return new GameData();
    }

    public void Save(GameData gameData)
    {
        Debug.Log("Saved game data to local");
    }
}