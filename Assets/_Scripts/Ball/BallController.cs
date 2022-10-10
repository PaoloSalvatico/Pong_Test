using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;

    [Header("Ball Stats")]
    [SerializeField] private float _ballSpeed;
    [SerializeField] [Range(1, 1.5f)]private float _ballAcceleration;
    [SerializeField] private Transform _ballStartingPos;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;

        Init();
    }

    public void Init()
    {
        transform.position = _ballStartingPos.position;

        var dir = 1;
        if (Random.value > .5f) dir = -1;
        AddStartForceMove(dir);
    }


    public void AddStartForceMove(float dir)
    {
        float y = Random.Range(-0.7f, .7f);
        Vector2 vector = new Vector2(dir, y);

        _rigidbody.velocity = vector * _ballSpeed;
    }

    public void AddForceMoveBorder()
    {
        Vector2 velocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector2(velocity.x, -velocity.y);
    }

    public void AddForceMovePlayer()
    {
        Vector2 velocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector2(-velocity.x, velocity.y) * _ballAcceleration;
    }
}
