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

    [Header("Pause Manager")]
    [SerializeField] private GameObject _pausePanel;

    [Header("Ball Manager")]
    [SerializeField] private BallController _ballPrefab;
    [SerializeField] private Transform _ballStartingPos;

    private int _actualTime;
    private BallController _ball;

    protected override void Awake()
    {
        base.Awake();
        Init();
        SpawnBallGame();
        SetPlayerMode();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnPausePerformed += OpenOrClosePausePanel;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnPausePerformed -= OpenOrClosePausePanel;
    }


    /// <summary>
    /// Set up timer, UI and score
    /// </summary>
    public void Init()
    {
        _actualTime = _startingTime;
        _timerText.text = _actualTime.ToString();
        _timerText.color = _greenColor;
        _player1Score.text = 0.ToString();
        _player2Score.text = 0.ToString();
        Time.timeScale = 1;
        StartCoroutine(Timer());
    }

    public void SpawnBallGame()
    {
        _ball = Instantiate(_ballPrefab);
    }

    /// <summary>
    /// Set player mode to the two different players, chosen in main menu
    /// </summary>
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
            GameManager.Instance.EndGame();
        }
    }

    /// <summary>
    /// Update UI after player score
    /// </summary>
    /// <param name="player">the player who scores</param>
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

    public void OpenOrClosePausePanel()
    {
        _pausePanel.SetActive(!_pausePanel.activeInHierarchy);
        Time.timeScale = _pausePanel.activeInHierarchy ? 0 : 1;
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.BackToMainMenu();
    }

    public Transform BallStartingPos { get => _ballStartingPos; set => _ballStartingPos = value; }
    public BallController Ball { get => _ball; set => _ball = value; }
}
