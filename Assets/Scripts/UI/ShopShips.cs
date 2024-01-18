using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal sealed class ShopShips : MonoBehaviour
{

    [SerializeField]
    GameObject levelSelector;

    [SerializeField] LoadShipData loadShipDataScript;

    [SerializeField] GameObject pagePrefab, pageParentLG;
    List<GameObject> pages = new List<GameObject>();

    private void Start()
    {
        CreatePages();
    }

    internal void CreatePages() 
    {
        foreach (ShipSO ship in loadShipDataScript.ships) 
        {
            GameObject g = Instantiate(pagePrefab,pageParentLG.transform);
            g.GetComponent<ShopPage>().SetShipData(ship);
            g.GetComponent<ShopPage>().shopShipsScript = this;
            pages.Add(g);
        }
    }

    internal void UpdateBuyingDataForAll()
    {
        for(int i=0;i< loadShipDataScript.ships.Length;i++)
        {
            pages[i].GetComponent<ShopPage>().SetBuyingData(loadShipDataScript.ships[i]);
        }
    }

    public void back() 
    {
        levelSelector.SetActive(true);
        gameObject.SetActive(false);
    }
}
