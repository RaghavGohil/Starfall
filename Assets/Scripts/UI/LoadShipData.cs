using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LoadShipData : MonoBehaviour
{
    public ShipSO[] ships;

    private void Start()
    {
        PurchaseShipsManager.purchasedShips = new bool[ships.Length];
        PurchaseShipsManager.equippedShipId = 0;

        for (int i = 0; i < PurchaseShipsManager.purchasedShips.Length; i++)
        {
            PurchaseShipsManager.purchasedShips[i] = false;
        }

        if (!PurchaseShipsManager.LoadData() && ships.Length != 0)
        {
            PurchaseShipsManager.purchasedShips[0] = true;
            PurchaseShipsManager.SaveData();
        }
    }
}
