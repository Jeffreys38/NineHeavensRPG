
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Game Data/Monster")]
public class MonsterSO : EntitySO
{
    [SerializeField] private int _receivedEXP;
    [SerializeField] private MonsterDropTableSO _monsterDropTable;
    
    public int ReceivedEXP => _receivedEXP;
    public MonsterDropTableSO MonsterDropTable => _monsterDropTable;
}