// This script manages the navigation part of the level selector.

using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

internal sealed class LevelSelectorNavigation : MonoBehaviour, IEndDragHandler
{

    [SerializeField] int maxPage;
    public int currPage { get; private set; }
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;

    [SerializeField] TMP_Text highScoreText;
    [SerializeField] LeanTweenType tweenType;

    LTDescr tween;

    float dragThreshold;

    void Awake()
    {
        currPage = 1;
        targetPos = levelPagesRect.transform.localPosition;
        dragThreshold = Screen.width / 15;
    }

    void Start() 
    {
        SetHighScoreText();
    }
    
    public void SetHighScoreText() 
    {
        highScoreText.text = "HIGHSCORE: "+ScoreManager.highScores[currPage-1].ToString(); 
    }

    public void Next()
    {
        if (currPage < maxPage) 
        {
            currPage++;
            targetPos += pageStep;
            MovePage();
        }
        SetHighScoreText();
    }

    public void Prev()
    {
        if (currPage > 1)
        {
            currPage--;
            targetPos -= pageStep;
            MovePage();
        }
        SetHighScoreText();
    }

    void MovePage()
    {
        if(tween != null)
            tween.reset();
        tween = levelPagesRect.LeanMoveLocal(targetPos,tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData pointerEventData) 
    {
        if (Mathf.Abs(pointerEventData.position.x - pointerEventData.pressPosition.x) > dragThreshold)
        {
            if (pointerEventData.position.x > pointerEventData.pressPosition.x) Prev();
            else Next();
        }
        else 
        {
            MovePage();
        }
    }

}
