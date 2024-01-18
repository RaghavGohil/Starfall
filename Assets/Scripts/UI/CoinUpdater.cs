using TMPro;
using UnityEngine;

public class CoinUpdater : MonoBehaviour
{

    [SerializeField] TMP_Text shopShipsCoinText;
    [SerializeField] TMP_Text levelSelectorCoinText;

    private void Start()
    {
        SetCoinText();
    }

    internal void SetCoinText()
    {
        shopShipsCoinText.text = levelSelectorCoinText.text = CoinManager.GetAmount().ToString();
    }
}
