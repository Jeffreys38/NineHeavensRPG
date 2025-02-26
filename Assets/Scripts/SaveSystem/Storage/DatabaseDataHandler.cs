using UnityEngine;

public class DatabaseDataHandler : IDataHandler
{
    public GameData Load()
    {
        // Test data
        return new GameData();
    }

    public void Save(GameData gameData)
    {
        Debug.Log("Saved game data to database");
    }
}