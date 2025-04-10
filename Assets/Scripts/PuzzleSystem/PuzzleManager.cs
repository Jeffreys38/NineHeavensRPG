using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO _onPuzzleCompleted;
    [SerializeField] private List<PuzzleSlot> slots;
    [SerializeField] private AngelStatue statue;

    private void OnEnable()
    {
        _onPuzzleCompleted.OnEventRaised += TryValidatePuzzle;
    }
    
    private void OnDisable()
    {
        _onPuzzleCompleted.OnEventRaised -= TryValidatePuzzle;
    }

    public void TryValidatePuzzle()
    {
        if (slots.Any(slot => !slot.IsOccupied))
            return;

        bool allCorrect = slots.All(slot => slot.IsCorrect());

        if (allCorrect)
            statue.OnPuzzleSolved();
        else
            statue.OnPuzzleFailed();
    }
}