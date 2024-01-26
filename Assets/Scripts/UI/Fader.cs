using System;
using UnityEngine;

internal sealed class Fader : MonoBehaviour
{
    public static Fader instance;

    CanvasGroup cg;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        cg.interactable = false;
        cg.alpha = 1f;
        FadeIn();
    }

    public void FadeIn() 
    {
        cg.blocksRaycasts = false;
        LeanTween.alphaCanvas(cg,0f,1f);    
    }
    public void FadeOut(Action OnComplete) 
    {
        cg.blocksRaycasts = true;
        LeanTween.alphaCanvas(cg,1f,1f)
            .setOnComplete(() => { OnComplete(); });
    }
}
