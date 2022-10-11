using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame(int i)
    {
        GameManager.Instance.StartGame(i);
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
