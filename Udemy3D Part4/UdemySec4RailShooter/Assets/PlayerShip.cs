using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : MonoBehaviour
{
    [Tooltip("In mts per sec")] [SerializeField] float speed = 10f;
    [Tooltip("In mts per sec")] [SerializeField] float xRange = 5f;
    [Tooltip("In mts per sec")] [SerializeField] float yRange = 3f;

    float xThrow, yThrow;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRawFactor = -20f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
       
    }

    void ProcessTranslation()
    {
        xThrow = /*en lugar de input*/ CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * speed * Time.deltaTime;

        float xRawPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(xRawPos, -xRange, xRange);



        yThrow = /*en lugar de input*/ CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * speed * Time.deltaTime;

        float yRawPos = transform.localPosition.y + yOffset;
        float clampedyPos = Mathf.Clamp(yRawPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedyPos, transform.localPosition.z);

    }

    void ProcessRotation()
    {
        float pitchDueToPos = transform.localPosition.y * positionPitchFactor;
        float pitchDueToCon = yThrow * controlPitchFactor;

        float pitch = pitchDueToPos + pitchDueToCon;

        float yaw = transform.localPosition.x * positionYawFactor; 

        float roll = xThrow * controlRawFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
