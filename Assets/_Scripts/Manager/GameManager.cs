using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    private string _scorePoints;
    private int _player1ScorePoints;
    private int _player2ScorePoints;
    private int _numberOfAIinGame;
    private string _winningPlayer;

    public void StartGame(int i)
    {
        _numberOfAIinGame = i;
        _player1ScorePoints = 0;
        _player2ScorePoints = 0;
        SceneManager.LoadScene(CostantVariables.FIELDSCENE);
    }

    public void EndGame()
    {
        if (_player1ScorePoints > _player2ScorePoints) _winningPlayer = CostantVariables.PLAYER1WINS;
        else if (_player1ScorePoints < _player2ScorePoints) _winningPlayer = CostantVariables.PLAYER2WINS;
        else _winningPlayer = CostantVariables.DRAW;

        ScorePoints = _player1ScorePoints + " - " + _player2ScorePoints;
        ScorePoints = ($"Score: {_player1ScorePoints} - {_player2ScorePoints}");

        SceneManager.LoadScene(CostantVariables.SUMMARYSCENE);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(CostantVariables.MAINMENUSCENE);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public string ScorePoints { get => _scorePoints; set => _scorePoints = value; }
    public int Player1ScorePoints { get => _player1ScorePoints; set => _player1ScorePoints = value; }
    public int Player2ScorePoints { get => _player2ScorePoints; set => _player2ScorePoints = value; }
    public int NumberOfAIinGame { get => _numberOfAIinGame; set => _numberOfAIinGame = value; }
    public string WinningPlayer { get => _winningPlayer; set => _winningPlayer = value; }
}
