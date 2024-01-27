using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class PausePanel : MonoBehaviour
{

    [SerializeField]
    float tweenTime;
    CanvasGroup pauseCG;
    [SerializeField]
    CanvasGroup gameControlCG;
    LTDescr pauseTweenInstance;
    [SerializeField] AsyncLoadManager asyncLoadManager;
    [SerializeField] AudioSource ambientAudioSource;

    private void Awake()
    {
        pauseCG = GetComponent<CanvasGroup>();
    }

    public void OpenPauseMenu()
    {
        gameControlCG.interactable = false;
        ambientAudioSource.Pause();
        if (pauseTweenInstance != null)
            LeanTween.cancel(pauseTweenInstance.id);
        pauseTweenInstance = LeanTween.value(gameObject, (value) => { pauseCG.alpha = value; }, pauseCG.alpha, 1f, tweenTime)
            .setOnComplete(()=>{Time.timeScale = 0f;}) ;
    }

    public void ResumeLevel()
    {
        ambientAudioSource.UnPause();
        gameControlCG.interactable = true;
        Time.timeScale = 1f;
        if (pauseTweenInstance != null)
            LeanTween.cancel(pauseTweenInstance.id);
        pauseTweenInstance = LeanTween.value(gameObject, (value) => { pauseCG.alpha = value; }, pauseCG.alpha, 0f, tweenTime)
            .setOnComplete(() => { gameObject.SetActive(false); });
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        StartCoroutine(asyncLoadManager.LoadAsync(SceneManager.GetActiveScene().buildIndex-1));
    }

    public void AllLevels()
    {
        Time.timeScale = 1f;
        Fader.instance.FadeOut(()=> { SceneManager.LoadScene("LevelSelection"); });
    }
}
