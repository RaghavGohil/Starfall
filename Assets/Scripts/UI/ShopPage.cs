using UnityEngine;
using TMPro;
using UnityEngine.UI;

internal sealed class ShopPage : MonoBehaviour
{
    ShipSO ship;
    [SerializeField] TMP_Text shipName;
    [SerializeField] TMP_Text description;
    [SerializeField] Image shipImage;
    [SerializeField] TMP_Text buyButtonText;
    [HideInInspector] public CoinUpdater coinUpdaterInstance;
    [HideInInspector] public ShopShips shopShipsScript;
    public void SetShipData(ShipSO s)
    {
        ship = s;
        shipName.text = ship.shipName;
        description.text = ship.description;
        shipImage.sprite = ship.shipImage;
        SetBuyingData(s);
    }

    public void SetBuyingData(ShipSO s) 
    {
        if (s.id == PurchaseShipsManager.equippedShipId)
            buyButtonText.text = "EQUIPPED";
        else if (s.price == 0 || PurchaseShipsManager.purchasedShips[s.id] == true)
            buyButtonText.text = "EQUIP";
        else
            buyButtonText.text = "BUY FOR " + s.price.ToString();
    }

    public void Buy() 
    {
        if (PurchaseShipsManager.purchasedShips[ship.id] == true)
        {
            PurchaseShipsManager.equippedShipId = ship.id;
            PurchaseShipsManager.SaveData();
            buyButtonText.text = "EQUIPPED";
            coinUpdaterInstance.SetCoinText();
            shopShipsScript.UpdateBuyingDataForAll();
        }
        else if (CoinManager.GetAmount() >= ship.price && ship.id != PurchaseShipsManager.equippedShipId)
        {
            CoinManager.DeductAmount(ship.price);
            PurchaseShipsManager.purchasedShips[ship.id] = true;
            PurchaseShipsManager.equippedShipId = ship.id;
            PurchaseShipsManager.SaveData();
            buyButtonText.text = "EQUIPPED";
            coinUpdaterInstance.SetCoinText();
            shopShipsScript.UpdateBuyingDataForAll();
        }

        foreach (bool b in PurchaseShipsManager.purchasedShips) { print(b); }
    }
}
