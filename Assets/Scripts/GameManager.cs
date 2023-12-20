// This script manages the entire game.

using UnityEngine;

internal sealed class GameManager : MonoBehaviour
{
    void Start()
    {
        LevelSelector.SetLevelLocks();    
    }

    void Update()
    {
        
    }
}
