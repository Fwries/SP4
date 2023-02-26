using UnityEngine;
using Photon.Pun;

public class coinBehaviour : MonoBehaviour
{

    private int coinAmount;
    [SerializeField] private AudioClip collected;
    public void SetUp(int amount)
    {
        coinAmount = amount;
    }

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().IncreaseCoins(coinAmount);
            SoundManager.Instance.PlaySound(collected);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
