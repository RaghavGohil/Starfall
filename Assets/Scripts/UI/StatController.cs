using TMPro;
using UnityEngine;

internal sealed class StatController : MonoBehaviour
{
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text scoreText;

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
        SetCoinText();
    }

    public void SetCoinText()
    {
        coinText.text = "COINS: " + CoinManager.GetAmount().ToString();
    }
    public void SetHealthText(int hp)
    {
        healthText.text = "HP: " + hp.ToString();
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "SCORE: " + score.ToString();
    }
}
