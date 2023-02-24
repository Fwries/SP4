using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    // Player stats
    private PlayerStats m_PlayerStats;

    // Variables affecting the player character m_Movement
    public float m_SPEED = 1.0f;
    private Vector3 m_CharacterDir;

    // Character's m_RigidBody
    private Rigidbody m_RigidBody;

    // Variables affecting the player character animation
    private Animator m_Animator;
    private Vector3 m_Movement;

    // Main camera and its components
    private Camera          m_MainCamera;
    private CameraSettings  m_CameraSettings;

    public AudioClip movementSound;

    private PhotonView photonView;

    [SerializeField] private Sprite player2Sprite;
    [SerializeField] private Sprite player3Sprite;

    float tintTimer;

    public Vector3 GetCharacterDir() { return m_CharacterDir; }

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
 

        if (photonView.Owner.ActorNumber == 2)
        {
            this.gameObject.transform.position = new Vector3(5.0f, 0.0f, 0.0f);
            //this.transform.GetChild(0).GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
            transform.GetComponentInChildren<SpriteRenderer>().sprite = player2Sprite;
            transform.GetComponentInChildren<Animator>().SetInteger("actorNumber", 1);
        }
        else if(photonView.Owner.ActorNumber == 3)
        {
            this.gameObject.transform.position = new Vector3(-5.0f, 0.0f, 0.0f);
            transform.GetComponentInChildren<SpriteRenderer>().sprite = player3Sprite;
            transform.GetComponentInChildren<Animator>().SetInteger("actorNumber", 2);
        }

        m_PlayerStats = gameObject.GetComponent<PlayerStats>();

        m_CharacterDir.Set(0.0f, 0.0f, 0.0f);

        m_RigidBody = gameObject.GetComponent<Rigidbody>();

        m_Animator = gameObject.GetComponentInChildren<Animator>();

        m_MainCamera = Camera.main;
        m_CameraSettings = m_MainCamera.GetComponent<CameraSettings>();

        tintTimer = 1f;
    }

    void UpdateMovementOnWASDPressed()
    {
        if (photonView.IsMine)
        {
            // The Horizontal and Vertical ranges change from 0 to +1 or -1 with increase/decrease in 0.05f steps - Unity
            // GetAxisRaw has changes from 0 to 1 or -1 immediately, so with no steps - Unity
            // Use GetAxisRaw if we dont want the player character to "slide" around for a while after updating
            float keyWS = Input.GetAxisRaw("Vertical");
            float keyAD = Input.GetAxisRaw("Horizontal");

            // Assign the inputs to m_Movement
            m_Movement.x = keyAD;
            m_Movement.z = keyWS;
            m_Movement.Normalize();

            // Update the character's sprite animation
            m_Animator?.SetFloat("MoveX", m_Movement.z);
            m_Animator?.SetFloat("MoveY", m_Movement.x);
            m_Animator?.SetFloat("Speed", m_Movement.sqrMagnitude);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                SoundManager.Instance.PlayWalk(movementSound);
            }
        }
    }

    void Update()
    {
        UpdateMovementOnWASDPressed();

        if (transform.GetComponentInChildren<SpriteRenderer>().color == new Color(1f, 1f, 1f))
        {
            tintTimer = 0f;
        }

        if (tintTimer < 0.3f)
        {
            tintTimer += Time.deltaTime;
        }
        else if (tintTimer > 0.3f && tintTimer < 0.4f)
        {
            transform.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            tintTimer = 1f;
        }
    }
    void UpdateCharacterOnMovementUpdated()
    {
        // Update the character direction
        m_CharacterDir = (m_SPEED + m_PlayerStats.Speed) * m_Movement;

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
