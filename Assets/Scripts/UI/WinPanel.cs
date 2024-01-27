using Game.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{

    [SerializeField]
    float tweenTime;
    CanvasGroup winPanelCG;

    [SerializeField]
    int levelToLoad;
    [SerializeField]
    CanvasGroup gameControlCG;

    [HideInInspector]public GameObject player;

    [SerializeField] AsyncLoadManager asyncLoadManager;

    [SerializeField] AudioSource ambientAudioSource;

    private void Start()
    {
        gameControlCG.interactable = false;
        ambientAudioSource.volume = 0f;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerMovement>().enabled = false;
        AudioManager.instance.PlayInGame("win");
        winPanelCG = GetComponent<CanvasGroup>();
        LevelManager.UnlockLevel(levelToLoad - 1);
        LeanTween.value(gameObject, (value) => { winPanelCG.alpha = value; }, winPanelCG.alpha, 1f, tweenTime);
    }

    public void NextLevel() 
    {
        StartCoroutine(asyncLoadManager.LoadAsync(levelToLoad));
    }
    public void EndGame() 
    {
        Fader.instance.FadeOut(() => { SceneManager.LoadScene("End"); });
    }
    public void ReplayLevel() 
    {
        StartCoroutine(asyncLoadManager.LoadAsync(SceneManager.GetActiveScene().buildIndex-1));
    }
}
