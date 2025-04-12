using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class LyricsManager : MonoBehaviour
{
    [Header("Listening To")]
    [SerializeField] private AudioSourceEventChannelSO _activeAudio;
    [SerializeField] private StringEventChannelSO _activeLyrics;
    [SerializeField] private VoidEventChannelSO _runLyrics;
    [SerializeField] private VoidEventChannelSO _pauseLyrics;
    
    public AudioSource audioSource;
    public LyricLoader lyricLoader;
    public TextMeshProUGUI lyricText;
    public List<LyricLine> lyrics;

    private int currentIndex = 0;
    private float timer = 0f;

    public bool autoStart = true;
    private bool isPlaying = false;
    private string path = "";
    private AudioClip audioClip;

    private void OnEnable()
    {
        _activeAudio.OnEventRaised += SetAudioSource;
        _activeLyrics.OnEventRaised += SetPath;
        _runLyrics.OnEventRaised += StartLyrics;
        _pauseLyrics.OnEventRaised += PauseLyrics;
    }

    private void OnDisable()
    {
        _activeAudio.OnEventRaised -= SetAudioSource;
        _activeLyrics.OnEventRaised -= SetPath;
        _runLyrics.OnEventRaised -= StartLyrics;
        _pauseLyrics.OnEventRaised -= PauseLyrics;
    }

    private void StartLyrics()
    {
        lyrics = lyricLoader.LoadLyrics(path);
        
        timer = 0f;
        currentIndex = 0;
        isPlaying = true;
        lyricText.text = "";
    }
    
    private void PauseLyrics()
    {
        isPlaying = false;
        lyricText.text = "";
    }

    void Update()
    {
        if (!isPlaying || currentIndex >= lyrics.Count)
            return;

        timer += Time.deltaTime;

        if (timer >= lyrics[currentIndex].time)
        {
            lyricText.text = lyrics[currentIndex].content;
            Debug.Log(lyrics[currentIndex].content + " at " + timer);
            currentIndex++;
        }
    }
    
    private void SetAudioSource(AudioSource audioSource) => this.audioSource = audioSource;
    private void SetPath(string path) => this.path = path;
}
