using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CraftableItem")]
public class CraftableItemSO : ItemSO
{
    [SerializeField] private List<ItemSO> _listIngredients = new List<ItemSO>(2);
    
    public List<ItemSO> ListIngredients => _listIngredients;
}