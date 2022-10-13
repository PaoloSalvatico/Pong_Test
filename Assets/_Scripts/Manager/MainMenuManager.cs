using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    /// <summary>
    /// MainMenu scene buttons functions
    /// </summary>

    public void StartGame(int numberOfAI)
    {
        GameManager.Instance.StartGame(numberOfAI);
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
