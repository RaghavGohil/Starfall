using Game.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{

    [SerializeField]
    float tweenTime;
    CanvasGroup losePanelCG;
    [SerializeField]
    CanvasGroup gameControlCG;
    [SerializeField] AsyncLoadManager asyncLoadManager;
    [SerializeField] AudioSource ambientAudioSource;

    private void Start()
    {
        gameControlCG.interactable = false;
        ambientAudioSource.volume = 0f;
        AudioManager.instance.PlayInGame("lose");
        losePanelCG = GetComponent<CanvasGroup>();
        LeanTween.value(gameObject, (value) => { losePanelCG.alpha = value; },losePanelCG.alpha,1f,tweenTime);
    }

    public void ReplayLevel()
    {
        StartCoroutine(asyncLoadManager.LoadAsync(SceneManager.GetActiveScene().buildIndex-1));
    }

    public void AllLevels()
    {
        Fader.instance.FadeOut(()=> { SceneManager.LoadScene("LevelSelection"); });
    }
}
