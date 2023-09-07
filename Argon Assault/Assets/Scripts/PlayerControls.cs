using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Speed and Range")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float xRange = 13f;
    [SerializeField] float yRange = 2f;

    [Header("Laser array")]
    [Tooltip("Add all player the lasers here")]
    [SerializeField] GameObject[] lasers;
    [SerializeField] Camera mainCamera;
    [SerializeField] Texture2D crosshair;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = -2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -10f;
    float xOffset, yOffset;
    float xMovement, yMovement;

    void Start()
    {
        Cursor.SetCursor(crosshair, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlMovement = yMovement * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlMovement;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xMovement * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    void ProcessTranslation()
    {
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");

        xOffset = xMovement * Time.deltaTime * moveSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yOffset = yMovement * Time.deltaTime * moveSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange + 4);

        transform.localPosition = new Vector3
        (clampedXPos,
         clampedYPos,
         transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if(Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            Vector3 cusrsorPosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(cusrsorPosition);
            Vector3 targetDirection = ray.direction;
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
            emissionModule.SetBurst(0,new ParticleSystem.Burst(0,1,1,1, 0.01f));
            emissionModule.SetBurst(1,new ParticleSystem.Burst(0,1,1,1, 0.01f));
            emissionModule.SetBurst(2,new ParticleSystem.Burst(0,1,1,1, 0.01f));
            laser.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
}
