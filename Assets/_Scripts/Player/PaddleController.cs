using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected Animator _animator;

    [Header("Paddle Data")]
    [SerializeField] private PlayerMode _playerMode;
    [SerializeField] private PaddleScriptableObj _playerData;
    [SerializeField] private PaddleScriptableObj _aiData;

    private float _inputY;
    private Vector2 _target;
    private Vector2 _moveAmount;
    private Vector2 _moveDirection;
    private Vector2 _smoothVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInParent<Animator>();
        _moveAmount = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BallController ball))
        {
            ball.PlayerAddForceMove(_inputY);
            _animator.SetTrigger("BallHitted");
        }
    }

    private void Update()
    {
        // Enable player 1 inputs
        if (_playerMode == PlayerMode.Player1)
        {
            _inputY = InputManager.Instance.Player1MoveValue.y;
        }

        // Enable Player 2 inputs
        else if(_playerMode == PlayerMode.Player2)
        {
            _inputY = InputManager.Instance.Player2MoveValue.y;
        }

        // Enable AI inputs
        else
        {
            if (UIFieldManager.Instance.Ball == null) return;
            _inputY = UIFieldManager.Instance.Ball.transform.position.y;
            _target = new Vector2(transform.position.x, _inputY);
            _moveAmount = Vector2.SmoothDamp(_moveAmount, _target, ref _smoothVelocity, .25f);
            return;
        }

        _moveDirection = new Vector2(0, _inputY).normalized;
    }

    private void FixedUpdate()
    {
        if(_playerMode == PlayerMode.AI)
        {
            float step = _aiData.paddleSpeed * Time.fixedDeltaTime;
            transform.position = Vector2.MoveTowards(_moveAmount, _target, step);
            return;
        }

        _rigidbody.velocity = _moveDirection * _playerData.paddleSpeed;
    }

    public PlayerMode PlayerMode { get => _playerMode; set => _playerMode = value; }
}
