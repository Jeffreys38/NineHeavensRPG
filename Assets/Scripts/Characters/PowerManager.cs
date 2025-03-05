public class PowerManager
{
    private ProtagonistStateSO _protagonistData;

    public PowerManager(ProtagonistStateSO protagonistData)
    {
        _protagonistData = protagonistData;
    }

    public int CalculatePower()
    {
        // int basePower = _protagonistData.level * 10;
        // int equipmentPower = 0;

        // foreach (var item in _protagonistData.equippedItems)
        // {
        //     equipmentPower += item.powerValue;
        // }

        // return basePower + equipmentPower;
        return 1;
    }
}