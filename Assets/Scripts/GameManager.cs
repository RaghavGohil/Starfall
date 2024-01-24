// This script manages the entire game.

using UnityEngine;
using System.Collections;

internal sealed class GameManager : MonoBehaviour
{
    [SerializeField] DialogueScreen dialogueScreen;
    [HideInInspector] public PlayerMovement playerMovementInstance;
    [SerializeField] WaveSystem waveSystemInstance;
    [SerializeField] CanvasGroup gameControlCG;
    LTDescr gameControlTween;
    public void StartGame()
    {
        StartCoroutine(StartSequence());
    }
    internal IEnumerator StartSequence() 
    {
        yield return new WaitForSeconds(dialogueScreen.messageTime*dialogueScreen.messages.Length);
        playerMovementInstance.StartMovement();
        waveSystemInstance.GenerateWave();
    }
}
