using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummaryManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _winnerText;
    [SerializeField] TextMeshProUGUI _scorePointsText;


    private void Awake()
    {
        _winnerText.text = GameManager.Instance.WinningPlayer;
        _scorePointsText.text = GameManager.Instance.ScorePoints;
    }

    /// <summary>
    /// Summaru scene buttons functions
    /// </summary>

    public void BackToMenu()
    {
        GameManager.Instance.BackToMainMenu();
    }

    public void Exit()
    {
        GameManager.Instance.ExitGame();
    }
}
