using UnityEngine;

internal sealed class ShopShips : MonoBehaviour
{

    [SerializeField]
    GameObject levelSelector;

    public void back() 
    {
        levelSelector.SetActive(true);
        gameObject.SetActive(false);
    }
}
