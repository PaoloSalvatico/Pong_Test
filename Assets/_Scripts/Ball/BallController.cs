using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trail;

    [Header("Ball Stats")]
    [SerializeField] private float _ballSpeed;
    [SerializeField] [Range(1, 1.5f)]private float _ballAcceleration;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _trail = GetComponent<TrailRenderer>();

        Init();
    }

    /// <summary>
    /// Set up ball position and random direction
    /// </summary>
    public void Init()
    {
        transform.position = UIFieldManager.Instance.BallStartingPos.position;
        _trail.enabled = true;

        var dir = 1;
        if (Random.value > .5f) dir = -1;
        AddStartForceMove(dir);
    }

    #region Add Force to ball Functions

    /// <summary>
    /// Functions to apply force to the ball: on the start, by border and by player
    /// </summary>
    public void AddStartForceMove(float dir)
    {
        float y = Random.Range(-1f, 1f);
        Vector2 vector = new Vector2(dir, y);

        _rigidbody.velocity = vector * _ballSpeed;
    }

    public void BorderAddForceMove()
    {
        Vector2 velocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector2(velocity.x, -velocity.y);
    }

    public void PlayerAddForceMove(float i)
    {
        Vector2 velocity = _rigidbody.velocity;
        float delta = 1;
        if (velocity.y < 0) delta = i * -1;
        if (velocity.y == Mathf.Epsilon) delta = Random.value;
        _rigidbody.velocity = new Vector2(-velocity.x, velocity.y * delta) * _ballAcceleration;
        ContactFeedback();
    }
    #endregion

    private void ContactFeedback()
    {
        var material = _spriteRenderer.material;
        material.color = Random.ColorHSV();
    }

    public TrailRenderer Trail { get => _trail; set => _trail = value; }
}
