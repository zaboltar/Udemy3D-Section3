using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : MonoBehaviour
{
    [Header("General")]
     [SerializeField] GameObject[] guns;

    [Header("Speed")]
    [Tooltip("In mts per sec")] [SerializeField] float speed = 10f;

    [Header("ScreenPosParam")]
    [Tooltip("In mts per sec")] [SerializeField] float xRange = 5f;
    [Tooltip("In mts per sec")] [SerializeField] float yRange = 3f;

    

    [Header("Controls")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRawFactor = -20f;
    float xThrow, yThrow;
    
    bool isCtrlEnabled = true;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

   void OnPlayerDeath () // called by string rfrence
    { 
        //freeze ctrls
        isCtrlEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCtrlEnabled) 
            {
                ProcessTranslation();
                ProcessRotation();
                ProcessFiring();
            }
      
       
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

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire")) 
        {
            SetGunsActive(true);
        } else 
        {
            SetGunsActive(false);
        }
    }

    void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns) //care with death fx
        {
           //gun.SetActive(isActive); 
           var gunEmission = gun.GetComponent<ParticleSystem>().emission;
           gunEmission.enabled = isActive;
        }
    }

  

}
