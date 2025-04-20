using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelectionScript : MonoBehaviour
{
    public void OnEasyButtonClick()
    {
        DifficultyLevelScript.Instance.StartEasyGame();
    }
    public void OnMediumButtonClick()
    {
        DifficultyLevelScript.Instance.StartMediumGame();
    }
    public void OnHardButtonClick()
    {
        DifficultyLevelScript.Instance.StartHardGame();
    }

}
