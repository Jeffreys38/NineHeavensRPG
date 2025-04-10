using UnityEngine;
using System.Collections;

public class ExecutionController : MonoBehaviour
{
    public int damage = 6000;
    public ExecutionAnimationHandler executionAnimationHandler;
    public GameSceneSO sceneToLoad;
    public GameSceneSO mainMenuScene;
    public AudioSource ambienceAudio;
    
    [Header("References")] 
    [SerializeField] private ProtagonistStateSO _player;
    
    [Header("Broadcasting On")]
    [SerializeField] private LoadEventChannelSO _loadLocation;
    [SerializeField] private LoadEventChannelSO _loadMainMenu;
    [SerializeField] private VoidEventChannelSO _executionEvent;
    [SerializeField] private IntEventChannelSO _takeDamageEvent;
    [SerializeField] private VoidEventChannelSO _resetGameEvent;
    
    private bool _hasExecuted = false;
    private ProtagonistController _protagonistController;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasExecuted) return;
        if (!other.CompareTag("Player")) return;
        
        _protagonistController = other.GetComponent<ProtagonistController>();
        StartExecution();
        _hasExecuted = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _hasExecuted = false;
    }

    private void StartExecution()
    {
        StartCoroutine(ExecuteSequence());
    }

    private IEnumerator ExecuteSequence()
    {
        ambienceAudio.Play();
        yield return new WaitForSeconds(0.5f);
        _protagonistController.BlockMovement();
        yield return new WaitForSeconds(7f);
        
        executionAnimationHandler.PlayExecutionAnimation();
        _takeDamageEvent.RaiseEvent(damage);
        
        yield return new WaitForSeconds(2f);
        EndExecution();
    }

    private void EndExecution()
    {
        if (_player.currentHealth > 0)
        {
            _protagonistController.UnlockMovement();
            _loadLocation.RaiseEvent(sceneToLoad, false, true);
        }
        else
        {
            _resetGameEvent.RaiseEvent();
            _loadMainMenu.RaiseEvent(mainMenuScene, false, true);
        }
    }
}