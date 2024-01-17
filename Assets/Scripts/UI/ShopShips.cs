using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal sealed class ShopShips : MonoBehaviour
{

    [SerializeField]
    GameObject levelSelector;

    [SerializeField] ShipSO[] ships;

    [SerializeField] TMP_Text shopShipsCoinText;
    [SerializeField] TMP_Text levelSelectorCoinText;

    [SerializeField] GameObject pagePrefab, pageParentLG;
    List<GameObject> pages = new List<GameObject>();

    private void Start()
    {

        SetCoinText();
        
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

        print(PurchaseShipsManager.purchasedShips);

        CreatePages();

    }

    internal void CreatePages() 
    {
        foreach (ShipSO ship in ships) 
        {
            GameObject g = Instantiate(pagePrefab,pageParentLG.transform);
            g.GetComponent<ShopPage>().SetShipData(ship);
            g.GetComponent<ShopPage>().shopShipsScript = this;
            pages.Add(g);
        }
    }

    internal void UpdateBuyingDataForAll()
    {
        for(int i=0;i<ships.Length;i++)
        {
            pages[i].GetComponent<ShopPage>().SetBuyingData(ships[i]);
        }
    }

    public void back() 
    {
        levelSelector.SetActive(true);
        gameObject.SetActive(false);
    }

    internal void SetCoinText()
    {
        shopShipsCoinText.text = levelSelectorCoinText.text = CoinManager.GetAmount().ToString();
    }
}
