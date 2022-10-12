using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;

    [SerializeField] Material _backgroundMaterial;

    [Header("Ball Stats")]
    [SerializeField] BallScriptableObj _data;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;

        Init();
    }

    /// <summary>
    /// Set up ball position and random direction
    /// </summary>
    public void Init()
    {
        transform.position = UIFieldManager.Instance.BallStartingPos.position;
        _data.material.color = Color.white;

        var dir = 1;
        if (Random.value > .5f) dir = -1;
        AddStartForceMove(dir);
    }

    #region Add Force to ball Functions

    /// <summary>
    /// Functions to apply force to the ball: on the start, by border and by player
    /// </summary>
    public void AddStartForceMove(float direction)
    {
        Vector2 vector = new Vector2(direction, 0);
        _rigidbody.velocity = vector * _data.ballSpeed;
    }

    public void BorderAddForceMove()
    {
        Vector2 velocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector2(velocity.x, -velocity.y);
    }

    public void PlayerAddForceMove(float paddleDirectionInput)
    {
        Vector2 velocity = _rigidbody.velocity;

        // Controll to avoid stall if both player are still and the ball's velocity.y is 0
        if (velocity.y == 0)
        {
            float random = Random.Range(0, 2);
            if(random == 0) velocity.y = Random.Range(-1, -.5f);
            if (random == 1) velocity.y = Random.Range(.5f, 1f);
        }

        Vector2 dir = new Vector2(-velocity.x, velocity.y);
        float bonusAcceleration = 1.2f;

        // Input towards down
        if(paddleDirectionInput < 0)
        {
            if (velocity.y > 0) bonusAcceleration *= -1;
            dir = new Vector2(-velocity.x, velocity.y * bonusAcceleration);
        }

        //Input towards up
        else if(paddleDirectionInput > 0)
        {
            if (velocity.y < 0) bonusAcceleration *= -1;
            dir = new Vector2(-velocity.x, velocity.y * bonusAcceleration);
        }

        _rigidbody.velocity = dir * _data.ballAcceleration;

        ContactFeedback();
    }
    #endregion

    private void ContactFeedback()
    {
        _data.material.color = Random.ColorHSV();
        while (_data.material.color == _backgroundMaterial.color)
        {
            _data.material.color = Random.ColorHSV();
        }
    }
}
