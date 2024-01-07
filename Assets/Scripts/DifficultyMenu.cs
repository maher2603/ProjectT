using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DifficultyMenu : MonoBehaviour
{
    public AICarController[] aiControllers; // Reference to AICarController instances

    // Set the difficulty level and adjust NPC car speeds
    public void SetDifficultyLevel(DifficultyLevel difficulty)
    {
        foreach (AICarController aiController in aiControllers)
        {
            aiController.SetDifficultyLevel(difficulty);
        }
    }
}