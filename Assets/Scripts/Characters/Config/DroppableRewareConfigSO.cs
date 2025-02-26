using System.Collections.Generic;
using UnityEngine;

public class DroppableRewardConfigSO
{
    [Tooltip("Item scattering distance from the source of dropping.")]
    [SerializeField] private float _scatteringDistance = default;
        
    [Tooltip("The list of drop group that can be dropped by this critter when killed")]
    [SerializeField] private List<DropGroup> _dropGroups = new List<DropGroup>();
}