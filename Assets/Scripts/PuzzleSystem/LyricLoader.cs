using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class LyricLine
{
    public float time;
    public string content;
}

[System.Serializable]
public class LyricWrapper
{
    public List<LyricLine> lyrics;
}

public class LyricLoader : MonoBehaviour
{
    public List<LyricLine> LoadLyrics(string filename)
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Lyrics/" + filename);
        if (!File.Exists(path))
        {
            Debug.LogError("Not found: " + path);
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<LyricWrapper>("{\"lyrics\":" + json + "}").lyrics;
    }
}