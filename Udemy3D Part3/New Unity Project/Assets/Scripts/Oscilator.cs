using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // :o 
public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;

    [Range(0,1)]
    [SerializeField]
    float movementFactor; // 0 for not moved, 1 for fully moved

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }
}
