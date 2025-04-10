using System;
using UnityEngine;

public enum SlotPosition { East, West, North, South }
public class PuzzleSlot : MonoBehaviour
{
    [Header("Broadcasting On")]
    [SerializeField] VoidEventChannelSO _puzzleCompletedEvent;
    
    [SerializeField] private SlotPosition slotPosition;
    [SerializeField] private StoneColor expectedColor;

    private PuzzleStone currentStone;

    public bool IsOccupied => currentStone != null;
    public SlotPosition SlotPosition => slotPosition;
    public StoneColor ExpectedColor => expectedColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var puzzleStone = other.GetComponent<PuzzleStone>();
        if (puzzleStone != null)
        {
            SetStone(puzzleStone);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        ClearStone();
    }

    private void SetStone(PuzzleStone stone)
    {
        currentStone = stone;
        _puzzleCompletedEvent.RaiseEvent();
    }

    private void ClearStone()
    {
        currentStone = null;
    }

    public bool IsCorrect()
    {
        return currentStone != null && currentStone.color == expectedColor;
    }
}