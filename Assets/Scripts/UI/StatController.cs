using TMPro;
using UnityEngine;

internal sealed class StatController : MonoBehaviour
{
    [SerializeField] TMP_Text coinText;

    public static StatController instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Start()
    {
        UpdateText();
    }

    internal void UpdateText() 
    {
        SetCoinText();
    }

    internal void SetCoinText()
    {
        coinText.text = "COINS: " + CoinManager.GetAmount().ToString();
    }
}
