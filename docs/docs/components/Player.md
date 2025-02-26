# Player Components

## Table of Contents

<!-- TOC -->
* [Player Components](#player-components)
  * [Table of Contents](#table-of-contents)
    * [PlayerSkillManager](#playerskillmanager)
    * [IDataStorage](#idatastorage)
<!-- TOC -->

### DataInitializer

**Purpose:**

- Responsible for initializing game data such as Player, Inventory, Achievements, etc.
- Ensures data is properly loaded when the game starts.
- Calls `DataRepository<T>` to manage data storage.

**Implementation:**

```csharp
public static class DataInitializer
{
    public static void InitializeData()
    {
        if (!DataRepository<PlayerData>.Exists())
        {
            DataRepository<PlayerData>.Save(new PlayerData());
        }
        
        if (!DataRepository<InventoryData>.Exists())
        {
            DataRepository<InventoryData>.Save(new InventoryData());
        }
    }
}
```

### IDataStorage

**Purpose:**

- Interface that defines a contract for all storage types.
- Ensures a consistent API for different data storage backends.

```csharp
public interface IDataStorage<T>
{
    void Save(T data);
    T Load();
}
```