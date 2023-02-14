using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float SPEED = 5.0f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void MovePlayerOnWASDPressed()
    {
        float keyWS = Input.GetAxis("Vertical");
        float keyAD = Input.GetAxis("Horizontal");

        // The translations are relative to the x-z plane
        float translateX = SPEED * keyWS * Time.deltaTime;
        float translateZ = SPEED * keyAD * Time.deltaTime;

        // Move the player and the main camera
        transform.Translate(translateZ, -translateX, 0.0f);
        mainCamera.transform.Translate(translateZ, 0.0f, translateX);
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
