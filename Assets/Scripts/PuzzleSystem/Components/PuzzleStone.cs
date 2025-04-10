using UnityEngine;
using DG.Tweening;

public enum StoneColor { Red, Blue, Green, Purple }

public class PuzzleStone : MonoBehaviour
{
    public StoneColor color;

    public void MoveTo(Vector2 targetPosition)
    {
        transform.DOMove(targetPosition, 0.15f).SetEase(Ease.OutQuad);
    }
}