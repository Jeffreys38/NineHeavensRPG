using System.IO;
using UnityEngine;

public class LocalDataHandler : IDataHandler
{
    private readonly string filePath;
    
    public LocalDataHandler(string filePath)
    {
        this.filePath = filePath;
    }

    public GameData Load()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found. Returning new GameData.");
            return new GameData();
        }
        
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error loading save file: {ex.Message}");
            return new GameData();
        }
    }

    public void Save(GameData gameData)
    {
        try
        {
            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(filePath, json);
            
            Debug.Log($"Save file saved to: {filePath}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error saving game data: {ex.Message}");
        }
    }
}