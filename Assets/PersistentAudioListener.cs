using UnityEngine;

public class PersistentAudioListener : MonoBehaviour
{
    private static PersistentAudioListener instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}