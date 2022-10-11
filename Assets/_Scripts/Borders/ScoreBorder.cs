using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBorder : MonoBehaviour
{
    [SerializeField] [Range(1, 2)] int _player;
    [SerializeField] BallController _ballController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BallController ball))
        {
            UIFieldManager.Instance.Score(_player);
            Destroy(ball.gameObject);
            StartCoroutine(SpawnNewBall(ball));
        }
    }

    private IEnumerator SpawnNewBall(BallController ball)
    {
        yield return new WaitForSeconds(.2f);
        UIFieldManager.Instance.SpawnBallGame();
    }
}
