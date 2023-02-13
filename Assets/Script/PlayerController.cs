using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float SPEED = 5.0f;

    private Camera          mainCamera;
    private CameraSettings  cameraSettings;

    void Start()
    {
        mainCamera = Camera.main;
        cameraSettings = mainCamera.GetComponent<CameraSettings>();
    }

    void MovePlayerOnWASDPressed()
    {
        float keyWS = Input.GetAxis("Vertical");
        float keyAD = Input.GetAxis("Horizontal");

        // The translations are relative to the x-z plane
        float translateX = SPEED * keyWS * Time.deltaTime;
        float translateZ = SPEED * keyAD * Time.deltaTime;

        // Move the player
        transform.Translate(translateZ, 0.0f, translateX);

        // Move the camera and adjust its look/forward vector
        mainCamera.transform.position = transform.position + new Vector3(cameraSettings.xOffsetFromPlayer, 
                                                                         cameraSettings.yOffsetFromPlayer,
                                                                         cameraSettings.zOffsetFromPlayer);
        mainCamera.transform.LookAt(transform);
    }

    void Update()
    {
        MovePlayerOnWASDPressed();
    }

    void ConstraintAxisMovement(Collision collision)
    {
        Transform playerTransform = collision.collider.transform;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        ConstraintAxisMovement(collision);
    }
}
