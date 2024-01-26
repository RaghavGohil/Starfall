using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

internal sealed class StatController : MonoBehaviour
{
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text winCoinText;
    [SerializeField] TMP_Text loseCoinText;
    [SerializeField] TMP_Text winScoreText;
    [SerializeField] TMP_Text loseScoreText;

    public static StatController instance { get; private set; }

    int sceneCoinAmount;
    public int sceneScoreAmount;
    [SerializeField]int setHighScoreOfIndex;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Start()
    {
        sceneCoinAmount = CoinManager.GetAmount();
        SetCoinText();
        SetScoreText();
    }

    public void SetCoinText()
    {
        coinText.text = "COINS: " + CoinManager.GetAmount().ToString();
        winCoinText.text = loseCoinText.text =  "COINS COLLECTED: "+(CoinManager.GetAmount()-sceneCoinAmount).ToString();
    }
    public void SetHealthText(int hp)
    {
        healthText.text = "HP: " + hp.ToString();
    }

    public void SetScoreText()
    {
        scoreText.text = winScoreText.text = loseScoreText.text = "SCORE: " + sceneScoreAmount.ToString();
        if (sceneScoreAmount > ScoreManager.highScores[setHighScoreOfIndex])
        { 
            ScoreManager.highScores[setHighScoreOfIndex] = sceneScoreAmount;
            ScoreManager.SaveData();
        }
    }
}
