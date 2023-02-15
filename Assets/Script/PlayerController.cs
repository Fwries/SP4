using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables affecting the player character m_Movement
    private const float m_SPEED = 5.0f;
    public int health = 17;

    // Variables affecting the player character animation
    private Animator m_Animator;
    private Vector2 m_Movement;

    // Main camera and its components
    private Camera          m_MainCamera;
    private CameraSettings  m_CameraSettings;

    void Start()
    {
        m_MainCamera = Camera.main;
        m_CameraSettings = m_MainCamera.GetComponent<CameraSettings>();

        m_Animator = gameObject.GetComponentInChildren<Animator>();
    }

    void MovePlayerOnWASDPressed()
    {
        float keyWS = Input.GetAxis("Vertical");
        float keyAD = Input.GetAxis("Horizontal");

        m_Movement.x = keyWS;
        m_Movement.y = keyAD;

        if (m_Movement.x != 0)
            m_Animator.SetFloat("MoveX", m_Movement.x);
        if (m_Movement.y != 0)
            m_Animator.SetFloat("MoveY", m_Movement.y);
        m_Animator.SetFloat("Speed", m_Movement.sqrMagnitude);

        // The translations are relative to the x-z plane
        float translateX = m_SPEED * keyWS * Time.deltaTime;
        float translateZ = m_SPEED * keyAD * Time.deltaTime;

        // Move the player
        transform.Translate(translateZ, 0.0f, translateX);

        // Move the camera and adjust its look/forward vector
        m_MainCamera.transform.position = transform.position + new Vector3(m_CameraSettings.xOffsetFromPlayer, 
                                                                         m_CameraSettings.yOffsetFromPlayer,
                                                                         m_CameraSettings.zOffsetFromPlayer);
        m_MainCamera.transform.LookAt(transform);

        // Fixed issue of character sill moving
        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        MovePlayerOnWASDPressed();
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision Enter");
    }

    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("Trigger Enter");
    }
}
