public interface IDataPersistence
{
    void SaveData(ref GameData data);
    void LoadData(GameData data);
}