using UnityEngine;

public class ExecutionAnimationHandler : MonoBehaviour
{
    public GameObject executorPrefab;
    public AudioSource audioSource;
    
    public void PlayExecutionAnimation()
    {
        Instantiate(executorPrefab, transform.position, Quaternion.identity);
        PlaySound();
    }

    private void PlaySound()
    {
        audioSource.Play();
    }
}