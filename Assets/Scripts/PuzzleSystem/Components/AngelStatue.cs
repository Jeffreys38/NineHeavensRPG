using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelStatue : MonoBehaviour
{
    public GameObject LocationEntrance;
    public List<GameObject> fireList;
    public AudioSource finishedAudio;

    public void OnPuzzleSolved()
    {
        Debug.Log("Puzzle Solved");
        StartCoroutine(ShowFiresSequentially());
    }

    public void OnPuzzleFailed()
    {
        Debug.Log("Puzzle Failed");
        LocationEntrance.SetActive(false);
    }

    private IEnumerator ShowFiresSequentially()
    {
        foreach (GameObject fire in fireList)
        {
            fire.SetActive(true);
            
            yield return new WaitForEndOfFrame();
            
            yield return new WaitForSeconds(2f);
        }
        
        if (finishedAudio != null)
        {
            finishedAudio.Play();
        }
        
        float delay = (finishedAudio != null && finishedAudio.clip != null) ? finishedAudio.clip.length : 2f;
        yield return new WaitForSeconds(3);

        LocationEntrance.SetActive(true);
    }
}