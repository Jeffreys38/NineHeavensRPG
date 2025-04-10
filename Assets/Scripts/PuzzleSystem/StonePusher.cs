using UnityEngine;

public class StonePusher : MonoBehaviour
{
    [SerializeField] private Vector2EventChannelSO _onPlayerMoveDirection;

    private PuzzleStone currentStone;
    private bool isTouched = false;
    private Transform playerTransform;

    private Vector2 currentMoveDirection = Vector2.zero;
    private bool isPushing = false;

    private void Start()
    {
        currentStone = GetComponent<PuzzleStone>();
        if (currentStone == null)
        {
            Debug.LogWarning("Stone not found, please check StonePusher (game object)");
        }
    }

    private void OnEnable()
    {
        _onPlayerMoveDirection.OnEventRaised += UpdateMoveDirection;
    }

    private void OnDisable()
    {
        _onPlayerMoveDirection.OnEventRaised -= UpdateMoveDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouched = true;
            playerTransform = collision.transform;
            StartPushing();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouched = false;
            playerTransform = null;
            StopPushing();
        }
    }

    private void UpdateMoveDirection(Vector2 moveDir)
    {
        currentMoveDirection = moveDir.normalized;
    }

    private void StartPushing()
    {
        if (!isPushing)
        {
            isPushing = true;
            StartCoroutine(PushRoutine());
        }
    }

    private void StopPushing()
    {
        isPushing = false;
        StopAllCoroutines();
    }

    private System.Collections.IEnumerator PushRoutine()
    {
        while (isPushing)
        {
            if (currentStone != null && currentMoveDirection != Vector2.zero)
            {
                Vector2 targetPosition = (Vector2)currentStone.transform.position + currentMoveDirection * 0.03f;
                currentStone.MoveTo(targetPosition);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
