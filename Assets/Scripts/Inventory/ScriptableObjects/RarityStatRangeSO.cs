using UnityEngine;

[CreateAssetMenu(fileName = "RarityStatRange", menuName = "Inventory/RarityStatRange")]
public class RarityStatRangeSO : ScriptableObject
{
    [Header("Common Ranges")]
    public Vector2 attackCommon;
    public Vector2 defenseCommon;
    public Vector2 intelligenceCommon;
    public Vector2 luckyCommon;

    [Header("Rare Ranges")]
    public Vector2 attackRare;
    public Vector2 defenseRare;
    public Vector2 intelligenceRare;
    public Vector2 luckyRare;

    [Header("Legendary Ranges")]
    public Vector2 attackLegendary;
    public Vector2 defenseLegendary;
    public Vector2 intelligenceLegendary;
    public Vector2 luckyLegendary;

    public Vector2 GetRange(Rarity rarity, BuffItemType type)
    {
        return rarity switch
        {
            Rarity.Common => type switch
            {
                BuffItemType.Attack => attackCommon,
                BuffItemType.Defense => defenseCommon,
                BuffItemType.Intelligence => intelligenceCommon,
                BuffItemType.Lucky => luckyCommon,
                _ => Vector2.zero
            },
            Rarity.Rare => type switch
            {
                BuffItemType.Attack => attackRare,
                BuffItemType.Defense => defenseRare,
                BuffItemType.Intelligence => intelligenceRare,
                BuffItemType.Lucky => luckyRare,
                _ => Vector2.zero
            },
            Rarity.Legendary => type switch
            {
                BuffItemType.Attack => attackLegendary,
                BuffItemType.Defense => defenseLegendary,
                BuffItemType.Intelligence => intelligenceLegendary,
                BuffItemType.Lucky => luckyLegendary,
                _ => Vector2.zero
            },
            _ => Vector2.zero
        };
    }
}