using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

internal sealed class DialogueScreen : MonoBehaviour
{
    [SerializeField]
    GameObject gameControlUI;
    CanvasGroup gameControlCanvasGroup;
    [SerializeField]
    GameObject dialogueScreen;
    [SerializeField]
    GameObject cinematicBar1;
    [SerializeField]
    GameObject cinematicBar2;

    RectTransform cinematicBarRect1, cinematicBarRect2;
    Vector2 anchoredPos1, anchoredPos2;

    [SerializeField]
    TMP_Text dialogueText;

    LeanTweenType tweenType;
    float tweenTime;

    [SerializeField]
    float moveOffset;

    Action dialogueComplete;

    void Start()
    {
        tweenTime = 1f;
        tweenType = LeanTweenType.easeOutCubic;

        cinematicBarRect1 = cinematicBar1.GetComponent<RectTransform>();
        cinematicBarRect2 = cinematicBar2.GetComponent<RectTransform>();

        anchoredPos1 = cinematicBarRect1.anchoredPosition;
        anchoredPos2 = cinematicBarRect2.anchoredPosition;

        dialogueComplete += DeactivateDialogueScreen;

        gameControlCanvasGroup = gameControlUI.GetComponent<CanvasGroup>();
        
        string[] message = { "Captain Ashish: So we have to destroy the alien ships to retrieve the alpha keys?", "Commander Sameer: Yes. All 5 planets store a key. If we retrieve it, we can revive the world.", "Captain Ashish: Sounds great!" , "Captain Sameer: You can dash through the floating blocks to get abilities.", "Captain Ashish: Yeah. It's the left button."};
        StartCoroutine(StartSequence(message,5f));
    }

    public IEnumerator StartSequence(string[] message, float duration) 
    {
        OpenDialogueScreen();
        byte count = 0;
        while (count < message.Length) 
        {
            dialogueText.text = message[count];
            yield return new WaitForSeconds(duration);
            count++;
        }
        CloseDialogueScreen();
    }

    void DeactivateDialogueScreen()
    {
        dialogueScreen.SetActive(false);
    }

    public void OpenDialogueScreen() 
    {
        dialogueScreen.SetActive(true);
        gameControlUI.SetActive(false);
        dialogueText.text = "";
        LeanTween.value(gameObject, 1f, 0f, tweenTime)
            .setEase(tweenType).setOnUpdate((value) =>
            {
                gameControlCanvasGroup.alpha = value;
            });
        LeanTween.value(gameObject, 0f, 1f, tweenTime)
            .setEase(tweenType).setOnUpdate((value) =>
            {
                dialogueText.alpha = value;
            });
        LeanTween.value(gameObject,anchoredPos1.y, anchoredPos1.y + cinematicBarRect1.rect.height + moveOffset,tweenTime)
            .setEase(tweenType).setOnUpdate((value) => 
            {
                cinematicBarRect1.anchoredPosition = new Vector2(anchoredPos1.x, value);
            });
        LeanTween.value(gameObject, anchoredPos2.y, anchoredPos2.y - cinematicBarRect2.rect.height - moveOffset, tweenTime)
            .setEase(tweenType).setOnUpdate((value) =>
            {
                cinematicBarRect2.anchoredPosition = new Vector2(anchoredPos2.x, value);
            });
    }
    public void CloseDialogueScreen() 
    {
        gameControlUI.SetActive(true);
        LeanTween.value(gameObject, 0f, 1f, tweenTime)
            .setEase(tweenType).setOnUpdate((value) =>
            {
                gameControlCanvasGroup.alpha = value;
            });
        LeanTween.value(gameObject, 1f, 0f, tweenTime)
            .setEase(tweenType).setOnUpdate((value) =>
            {
                dialogueText.alpha = value;
            });
        LeanTween.value(gameObject, anchoredPos1.y + cinematicBarRect1.rect.height + moveOffset, anchoredPos1.y, tweenTime)
            .setEase(tweenType).setOnUpdate((value) =>
            {
                cinematicBarRect1.anchoredPosition = new Vector2(anchoredPos1.x, value);
            });
        LeanTween.value(gameObject, anchoredPos2.y - cinematicBarRect2.rect.height - moveOffset, anchoredPos2.y, tweenTime)
            .setEase(tweenType).setOnUpdate((value) =>
            {
                cinematicBarRect2.anchoredPosition = new Vector2(anchoredPos2.x, value);
            }).setOnComplete(dialogueComplete);
    }
}
