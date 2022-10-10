using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBorder : MonoBehaviour
{
    [SerializeField] [Range(1, 2)] int _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BallController ball))
        {
            UIFieldManager.Instance.Score(_player);
            ball.Init();
        }
    }
}
