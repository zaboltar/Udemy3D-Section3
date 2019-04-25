using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject prefabDeathFX;
    [SerializeField] Transform parent;
    ScoreBoard scoreBoard;

    [SerializeField] int scorePerHit = 12;
    
    void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();

        
    }


    void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(scorePerHit);
        GameObject fx = Instantiate(prefabDeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject); 
        
    }

    void AddBoxCollider()
    {
        Collider boxCol = gameObject.AddComponent<BoxCollider>();
        boxCol.isTrigger = false;
    }

}
