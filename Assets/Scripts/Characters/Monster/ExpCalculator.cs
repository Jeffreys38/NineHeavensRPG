using UnityEngine;

public class ExpCalculator
{
    public static int CalculateExpGain(RealmTier playerTier, MonsterSO enemy)
    {
        int exp = enemy.ReceivedEXP;
        int levelDifference = (int)playerTier - (int)enemy.RealmTier;

        float expMultiplier = levelDifference switch
        {
            0 => 1.0f,  // Equal => 100% EXP
            1 => 0.75f, // Greater than 1 level => 75% EXP
            2 => 0.5f,  // Greater than 2 level => 50% EXP
            3 => 0.25f, // Greater than 3 level => 25% EXP
            _ => 0.1f   // Greater than 4 level => 10% EXP
        };

        return Mathf.RoundToInt(exp * expMultiplier);
    }
}