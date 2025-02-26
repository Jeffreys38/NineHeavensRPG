using UnityEngine;

namespace JeffreyInc.Characters
{
    public class Damageable : MonoBehaviour
    {
        [Header("Health")]
        
        
        [Header("Combat")]
        [SerializeField] private DroppableRewardConfigSO _droppableRewardSO;
        
        [Header("Broadcasting On")]
        [SerializeField] private VoidEventChannelSO _updateHealthUI = default;
        [SerializeField] private VoidEventChannelSO _deathEvent = default;
        
        public DroppableRewardConfigSO DroppableRewardConfig => _droppableRewardSO; // Used for other script: DropRewardSO, etc
    }
}