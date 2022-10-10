using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFieldManager : Singleton<UIFieldManager>
{
    [Header("Score Text")]
    [SerializeField] TextMeshProUGUI _player1Score;
    [SerializeField] TextMeshProUGUI _player2Score;
    [SerializeField] TextMeshProUGUI _timerText;

    [Header("Time Manager")]
    [SerializeField] private int _startingTime;
    [SerializeField] private float _redTime;
    [SerializeField] private Color _greenColor;
    [SerializeField] private Color _redColor;

    [Header("Player Mode")]
    [SerializeField] private List<PaddleController> _playerList;

    private int _actualTime;

    private void Start()
    {
        Init();
        SetPlayerMode();
    }

    public void Init()
    {
        _actualTime = _startingTime;
        _timerText.text = _actualTime.ToString();
        _timerText.color = _greenColor;
        _player1Score.text = 0.ToString();
        _player2Score.text = 0.ToString();
        StartCoroutine(Timer());
    }

    public void SetPlayerMode()
    {
        var manager = GameManager.Instance;
        for(int i = 0; i < manager.NumberOfAIinGame; i++)
        {
            _playerList[i]._playerMode = PlayerMode.AI;
        }
        if(manager.NumberOfAIinGame == 0)
        {
            _playerList[0]._playerMode = PlayerMode.Player1;
            _playerList[1]._playerMode = PlayerMode.Player2;
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        _actualTime--;
        _timerText.text = _actualTime.ToString();
        _timerText.color = _actualTime < _redTime ? _redColor : _greenColor;
        if (_actualTime > 0)
        {
            StartCoroutine(Timer());
        }
        else
        {
            // TODO load scene by Game manager passing who won
        }
    }

    public void Score(int player)
    {
        GameManager manager = GameManager.Instance;
        if(player == 1)
        {
            manager.Player1ScorePoints++;
            _player1Score.text = manager.Player1ScorePoints.ToString();
        }
        else
        {
            manager.Player2ScorePoints++;
            _player2Score.text = manager.Player2ScorePoints.ToString();
        }
    }
}
