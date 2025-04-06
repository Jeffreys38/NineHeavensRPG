using TMPro;
using UnityEngine;

public class UIPlayerDetails : MonoBehaviour
{
        [SerializeField] private ProtagonistStateSO _player;
        
        [Header("References")] 
        [SerializeField] private TextMeshProUGUI baseAttack;
        [SerializeField] private TextMeshProUGUI baseDefense;
        [SerializeField] private TextMeshProUGUI baseLucky;
        [SerializeField] private TextMeshProUGUI baseIntelligence;
        
        [SerializeField] private TextMeshProUGUI bonusAttack;
        [SerializeField] private TextMeshProUGUI bonusDefense;
        [SerializeField] private TextMeshProUGUI bonusLucky;
        [SerializeField] private TextMeshProUGUI bonusIntelligence;

        void OnEnable()
        {
                baseAttack.SetText(_player.currentAtk.ToString());
                baseDefense.SetText(_player.currentAtk.ToString());
                baseLucky.SetText(_player.currentAtk.ToString());
                baseIntelligence.SetText(_player.currentAtk.ToString());
                
                bonusAttack.SetText($"(+{_player.bonusAtk})");
                bonusDefense.SetText($"(+{_player.bonusDefense})");
                bonusLucky.SetText($"(+{_player.bonusAtk})");
                bonusIntelligence.SetText($"(+{_player.bonusAtk})");
        }
}