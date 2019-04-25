using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject prefabDeathFX;
    [SerializeField] Transform parent;
    
    void Start()
    {
        Collider boxCol = gameObject.AddComponent<BoxCollider>();
        boxCol.isTrigger = false;
    }


    void OnParticleCollision(GameObject other)
    {
        GameObject fx = Instantiate(prefabDeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }

}
