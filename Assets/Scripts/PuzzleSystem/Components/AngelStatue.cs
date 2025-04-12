using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelStatue : MonoBehaviour
{
    [Header("Broadcasting On")]
    [SerializeField] private FadeChannelSO fadeChannelSO;
    [SerializeField] private BoolEventChannelSO _setHUDStatus = default;
    [SerializeField] private StringEventChannelSO _activeLyrics;
    [SerializeField] private VoidEventChannelSO _runLyrics;
    [SerializeField] private LoadEventChannelSO _loadMenu;
    
    public GameObject hintUI;
    public GameObject locationEntrance;
    public List<GameObject> fireList;
    public AudioSource finishedAudio;

    void Start()
    {
        _activeLyrics.RaiseEvent("MotBuocYeuVanDamDau.json");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        hintUI.SetActive(true);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        hintUI.SetActive(false);
    }

    public void OnPuzzleSolved()
    {
        Debug.Log("Puzzle Solved");
        StartCoroutine(PlayMemories());
    }

    private IEnumerator PlayMemories()
    {
        yield return ShowFiresSequentially();
    }

    public void OnPuzzleFailed()
    {
        Debug.Log("Puzzle Failed");
        locationEntrance.SetActive(false);
    }

    private IEnumerator ShowFiresSequentially()
    {
        // 1. Light up each fire one by one with a delay
        foreach (GameObject fire in fireList)
        {
            fire.SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }

        // 2. Fade the screen to black over 3 seconds
        fadeChannelSO.FadeOut(3);
        _setHUDStatus.RaiseEvent(false);
        yield return new WaitForSeconds(3f);
        
        // 6. Fade the screen back in over 2 seconds
        fadeChannelSO.FadeIn(2);
        finishedAudio.Play();
        yield return new WaitForSeconds(1f);
        _runLyrics.RaiseEvent();
        
        // 7. After a delay, unlock the entrance to proceed
        yield return new WaitForSeconds(4f);
        locationEntrance.SetActive(true);
    }
}