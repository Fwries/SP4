using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player stats
    private PlayerStats m_PlayerStats;

    // Variables affecting the player character m_Movement
    private const float m_SPEED = 5.0f;
    private Vector3 m_CharacterDir;

    // Character's m_RigidBody
    private Rigidbody m_RigidBody;

    // Variables affecting the player character animation
    private Animator m_Animator;
    private Vector3 m_Movement;

    // Main camera and its components
    private Camera          m_MainCamera;
    private CameraSettings  m_CameraSettings;

    public Vector3 GetCharacterDir() { return m_CharacterDir; }

    void Start()
    {
        m_PlayerStats = gameObject.GetComponent<PlayerStats>();

        m_CharacterDir.Set(0.0f, 0.0f, 0.0f);

        m_RigidBody = gameObject.GetComponent<Rigidbody>();

        m_Animator = gameObject.GetComponentInChildren<Animator>();

        m_MainCamera = Camera.main;
        m_CameraSettings = m_MainCamera.GetComponent<CameraSettings>();
    }

    void UpdateMovementOnWASDPressed()
    {
        // The Horizontal and Vertical ranges change from 0 to +1 or -1 with increase/decrease in 0.05f steps - Unity
        // GetAxisRaw has changes from 0 to 1 or -1 immediately, so with no steps - Unity
        // Use GetAxisRaw if we dont want the player character to "slide" around for a while after updating
        float keyWS = Input.GetAxis("Vertical");
        float keyAD = Input.GetAxis("Horizontal");

        // Assign the inputs to m_Movement
        m_Movement.x = keyAD;
        m_Movement.z = keyWS;
        m_Movement.Normalize();

        // Update the character's sprite animation
        m_Animator?.SetFloat("MoveX", m_Movement.z);
        m_Animator?.SetFloat("MoveY", m_Movement.x);
        m_Animator?.SetFloat("Speed", m_Movement.sqrMagnitude);
    }

    void Update()
    {
        UpdateMovementOnWASDPressed();
    }
    void UpdateCharacterOnMovementUpdated()
    {
        // Update the character direction
        m_CharacterDir = m_SPEED * m_Movement;

        // Move the character
        m_RigidBody?.MovePosition(m_RigidBody.position + m_CharacterDir * Time.fixedDeltaTime);

        // Move the camera and adjust its look/forward vector
        m_MainCamera.transform.position = m_RigidBody.position + new Vector3(m_CameraSettings.xOffsetFromPlayer,
                                                                             m_CameraSettings.yOffsetFromPlayer,
                                                                             m_CameraSettings.zOffsetFromPlayer);
        m_MainCamera.transform.LookAt(m_RigidBody.position);

        // Fixed issue of character sill moving
        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        UpdateCharacterOnMovementUpdated();
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("PlayerController: Collision Enter");
    }

    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("PlayerController: Trigger Enter");
    }
}
