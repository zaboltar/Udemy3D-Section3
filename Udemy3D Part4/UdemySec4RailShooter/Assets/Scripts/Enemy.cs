using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject prefabDeathFX;
    [SerializeField] Transform parent;
    ScoreBoard scoreBoard;

    [SerializeField] int scorePerHit = 12;
    [SerializeField] int hits = 5;
    
    void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();

        
    }


    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        
        if (hits <= 1 ) 
        {
            KillEnemy();
        }
        
        
        
    }

    void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits = hits - 1;
    }
    void AddBoxCollider()
    {
        Collider boxCol = gameObject.AddComponent<BoxCollider>();
        boxCol.isTrigger = false;
    }

    void KillEnemy()
    {
        GameObject fx = Instantiate(prefabDeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject); 
    }

}
