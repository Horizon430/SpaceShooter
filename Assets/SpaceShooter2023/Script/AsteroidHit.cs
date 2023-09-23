using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHit : MonoBehaviour
{
    [SerializeField] private GameObject asteroidExplosion;
    [SerializeField] private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void AsteroidDestroyed()
    {
        Instantiate(asteroidExplosion, transform.position, transform.rotation);

        if(GameController.currentGameStatus == GameController.GameState.Playing)
        {
            //calculate the score for hitting this asteroid.
            float distanceFromPlayer = Vector3.Distance(transform.position, Vector3.zero);
            int bonusPoints = (int)distanceFromPlayer;

            int asteroidScore = 10 * bonusPoints;
            //pass score to GameController
            gameController.UpdatePlayerScore(asteroidScore);
        }    

        

        Destroy(this.gameObject);
    }
  //Design Operations Coordinator
}
