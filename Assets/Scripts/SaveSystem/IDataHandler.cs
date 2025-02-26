public interface IDataHandler
{
    GameData Load();
    void Save(GameData gameData);
}