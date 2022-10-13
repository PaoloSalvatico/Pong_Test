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

    private void OnEnable()
    {
        InputManager.Instance.OnMovePerformed += PerformPlayerMovement;
        InputManager.Instance.OnStopMovePerformed += PerformStopPlayerMovement;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnMovePerformed -= PerformPlayerMovement;
        InputManager.Instance.OnStopMovePerformed -= PerformStopPlayerMovement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BallController ball))
        {
            ball.PlayerAddForceMove(_rigidbody.velocity.y);
            _animator.SetTrigger("BallHitted");
        }
    }

    /// <summary>
    /// Function called by the input event OnMovePerformed
    /// </summary>
    private void PerformPlayerMovement()
    {
        if (_playerMode == PlayerMode.AI) return;

        if (_playerMode == PlayerMode.Player1)
        {
            _inputY = InputManager.Instance.Player1MoveValue.y;
        }

        else if (_playerMode == PlayerMode.Player2)
        {
            _inputY = InputManager.Instance.Player2MoveValue.y;
        }

        _moveDirection = new Vector2(0, _inputY).normalized;
        _rigidbody.velocity = _moveDirection * _playerData.paddleSpeed;
    }

    /// <summary>
    /// Function called by the input event OnStopMovePerformed
    /// </summary>
    private void PerformStopPlayerMovement()
    {
        if (_playerMode == PlayerMode.AI) return;

        _rigidbody.velocity = Vector2.zero;
    }

    private void Update()
    {
        //Enable AI movements
        if (_playerMode == PlayerMode.AI)
        {
            if (UIFieldManager.Instance.Ball == null) return;

            _inputY = UIFieldManager.Instance.Ball.transform.position.y;
            _target = new Vector2(transform.position.x, _inputY);
            _moveAmount = Vector2.SmoothDamp(_moveAmount, _target, ref _smoothVelocity, .25f);
        }
    }

    private void FixedUpdate()
    {
        //Actual AI movements
        if(_playerMode == PlayerMode.AI)
        {
            float step = _aiData.paddleSpeed * Time.fixedDeltaTime;
            transform.position = Vector2.MoveTowards(_moveAmount, _target, step);
        }
    }

    public PlayerMode PlayerMode { get => _playerMode; set => _playerMode = value; }
}
