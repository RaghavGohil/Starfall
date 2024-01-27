using Game.Sound;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationSequencer : MonoBehaviour
{
    [SerializeField] GameObject[] animationPanels;
    [SerializeField] float panelTime;
    [SerializeField] float endFrameTime;
    [SerializeField] string sceneToLoad;
    int index;
    private void Start()
    {
        index = 0;
        DisableAllPanels(); 
        StartCoroutine(StartAnimation());
    }
    internal void DisableAllPanels() 
    {
        foreach (GameObject g in animationPanels) 
        {
            g.SetActive(false); 
        }
    }
    IEnumerator StartAnimation()
    {
        while(index < animationPanels.Length)
        {
            DisableAllPanels(); 
            animationPanels[index].SetActive(true);
            AudioManager.instance.PlayInGame("dialogue");
            yield return new WaitForSeconds(panelTime);
            index++;
        }
        yield return new WaitForSeconds(endFrameTime);
        Fader.instance.FadeOut(() => { SceneManager.LoadScene(sceneToLoad); });
    }
}
