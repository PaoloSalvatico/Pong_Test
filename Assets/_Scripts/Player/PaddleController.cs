using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    public PlayerMode _playerMode;

    [SerializeField] protected float _paddleSpeedPlayer;
    [SerializeField] protected float _paddleSpeedAI;
    [SerializeField] protected BallController _ball;

    private float inputY;
    private Vector2 target;
    private Vector2 _moveAmount;
    private Vector2 _moveDirection;
    private Vector2 _smoothVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveAmount = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BallController ball))
        {
            ball.AddForceMovePlayer();
        }
    }

    private void Update()
    {
        //Enable player 1 inputs
        if (_playerMode == PlayerMode.Player1)
        {
            inputY = InputManager.Instance.Player1MoveValue.y;
        }
        //Enable Player 2 inputs
        else if(_playerMode == PlayerMode.Player2)
        {
            inputY = InputManager.Instance.Player2MoveValue.y;
        }
        //Enable AI inputs
        else
        {
            inputY = _ball.transform.position.y;
            target = new Vector2(transform.position.x, inputY);
            _moveAmount = Vector2.SmoothDamp(_moveAmount, target, ref _smoothVelocity, .4f);
            return;
        }

        _moveDirection = new Vector2(0, inputY).normalized;
    }

    private void FixedUpdate()
    {
        if(_playerMode == PlayerMode.AI)
        {
            float step = _paddleSpeedAI * Time.fixedDeltaTime;
            transform.position = Vector2.MoveTowards(_moveAmount, target, step);
            return;
        }
        _rigidbody.velocity = _moveDirection * _paddleSpeedPlayer;
    }
}
