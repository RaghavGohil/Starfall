// This script manages the navigation part of the level selector.

using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelectorNavigation : MonoBehaviour, IEndDragHandler
{

    [SerializeField] int maxPage;
    int currPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;

    [SerializeField] LeanTweenType tweenType;

    float dragThreshold;

    void Awake()
    {
        currPage = 1;
        targetPos = levelPagesRect.transform.localPosition;
        dragThreshold = Screen.width / 15;
    }

    public void Next()
    {
        if (currPage < maxPage) 
        {
            currPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Prev()
    {
        if (currPage > 1)
        {
            currPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos,tweenTime).setEase(tweenType);
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