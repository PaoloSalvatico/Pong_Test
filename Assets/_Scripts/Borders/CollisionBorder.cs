using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BallController ball))
        {
            ball.AddForceMoveBorder();
        }
    }
}
