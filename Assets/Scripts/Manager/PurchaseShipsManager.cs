using System.ComponentModel;
using Unity.VisualScripting;

internal static class PurchaseShipsManager
{

    public static bool[] purchasedShips;
    public static int equippedShipId;

    static PurchaseShipsManager() 
    {
        equippedShipId = 0;
    }

    public static bool LoadData()
    {
        object purchased_ships = SerializationManager.Load("purchased_ships");
        object equipped_ship_id = SerializationManager.Load("equipped_ship_id");
        if (purchased_ships != null && equipped_ship_id != null)
        {
            purchasedShips = (bool[])purchased_ships;
            equippedShipId = (int)equipped_ship_id;
            return true;
        }
        else
            return false;
    }

    public static void SaveData()
    {
        SerializationManager.Save("purchased_ships", purchasedShips);
        SerializationManager.Save("equipped_ship_id", equippedShipId);
    }
}