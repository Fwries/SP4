using UnityEngine;
using Photon.Pun;

public class coinBehaviour : MonoBehaviour
{
    private PhotonView m_PhotonView;
    private int coinAmount;
    [SerializeField] private AudioClip collected;
    public void SetUp(int amount)
    {
        coinAmount = amount;
    }

    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!m_PhotonView.IsMine)
            return;

        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().IncreaseCoins(coinAmount);
            SoundManager.Instance.PlaySound(collected);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
