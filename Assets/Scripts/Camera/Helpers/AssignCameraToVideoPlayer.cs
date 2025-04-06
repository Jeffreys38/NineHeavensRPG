using UnityEngine;
using UnityEngine.Video;

public class AssignCameraToVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.targetCamera = Camera.main;
    }
}