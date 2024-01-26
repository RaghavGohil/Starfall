// This script manages the entire game.

using UnityEngine;
using System.Collections;

internal sealed class GameManager : MonoBehaviour
{
    [SerializeField] DialogueScreen dialogueScreen;
    [HideInInspector] public PlayerMovement playerMovementInstance;
    [SerializeField] CanvasGroup gameControlCG;
    LTDescr gameControlTween;

    private void Awake()
    {
        GetComponent<WaveSystem>().enabled = false;
    }
    public void StartGame()
    {
        StartCoroutine(StartSequence());
    }
    internal IEnumerator StartSequence() 
    {
        yield return new WaitForSeconds(dialogueScreen.messageTime*dialogueScreen.messages.Length);
        playerMovementInstance.StartMovement();
        GetComponent<WaveSystem>().enabled = true;
    }
}
