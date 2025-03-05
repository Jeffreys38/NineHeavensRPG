using System.Collections.Generic;
using UnityEngine;

public class LocalDataHandler : IDataHandler
{
    public GameData Load()
    {
        GameData data = new GameData();
        data.protagonistData = new ProtagonistData();
        // data.protagonistData.learnedSkills = new List<string>()
        // {
        //     "aceea84dc1e30ab4abf90d2d0d845ada", 
        //     "aceea84dc1e30ab4abf90d2d0d845ada", 
        //     "aceea84dc1e30ab4abf90d2d0d845ada", 
        //     "aceea84dc1e30ab4abf90d2d0d845ada"
        // };
        
        return data;
    }

    public void Save(GameData gameData)
    {
        Debug.Log("Saved game data to local");
    }
}