using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameConstants
{
    [Header("Basic information")]
    public static string VERSION = "1.0.0";
        
    [Header("Game Settings")]
    public static readonly string DEFAULT_NAME = "Default";
    public static readonly int MAX_PLAYER_LEVEL = 70;

    [Header("Chat/Social")] 
    public static readonly int MAX_FRIENDSHIPS = 10;
    public static readonly int MAX_CHAT_HISTORY = 40;
        
    [Header("Inventory")]
    public static readonly int INVENTORY_MAX_EQUIPMENT = 500;
    public static readonly int INVENTORY_MAX_MATERIAL = 1500;

    public static readonly bool useDatabase = false;
}