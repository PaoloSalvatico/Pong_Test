using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    private int _player1ScorePoints;
    private int _player2ScorePoints;

    private string _winningPlayer;
    private int _numberOfAIinGame;


    public void EndMatch()
    {
        //switch
        // _winningPlayer = Player 1, player 2, It's a draw

        //then load summary scene that will stamp the message _winningPlayer
    }

    public void StartGame(int i)
    {
        _numberOfAIinGame = i;
        SceneManager.LoadScene("Field");
    }

    public int Player1ScorePoints { get => _player1ScorePoints; set => _player1ScorePoints = value; }
    public int Player2ScorePoints { get => _player2ScorePoints; set => _player2ScorePoints = value; }
    public int NumberOfAIinGame { get => _numberOfAIinGame; set => _numberOfAIinGame = value; }
}
