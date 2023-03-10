using UnityEngine;
using Photon.Pun;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;

    public string Username;
    public int MaxHealth = 20;
    public int currentHealth;
    public int Coin;

    public float AtkSpeed;
    public int Reach;
    public float Speed;
    public int Regen;
    public int Dodge;
    public int CritChance;
    public int LifeSteal;
    public int ResistDamage;

    public HealthBarBehaviour HealthBar;
    public coinCounter coinCounter;
    public GameObject dodgeText;

    private float Cooldown;
    private PhotonView photonView;

    [SerializeField] private AudioClip playerHurt;

    private void Start()
    {
        HealthBar = GameObject.Find("HealthBar").GetComponent<HealthBarBehaviour>();
        coinCounter = GameObject.Find("Coins").GetComponent<coinCounter>();
        MainCamera = Camera.main;

        currentHealth = MaxHealth;
        HealthBar.SetMaxHealth(currentHealth);
    }
    private void Awake()
    {
        photonView = this.transform.GetComponent<PhotonView>();
    }

    void Update()
    {
        Cooldown += Time.deltaTime;
        if (Cooldown >= 20)
        {
            if (Regen > 0)
            {
                RecoverHealth(Regen);
            }

            Cooldown = 0;
        }
        if (currentHealth <= 0)
        {
            HealthBar.CheckDeath();
        }
    }
    public void RecoverHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        HealthBar.SetHealth(currentHealth);
    }

    public void EquipmentEquip(ScEquipment scEquipment)
    {
        AtkSpeed += scEquipment.AtkSpeedAdd;
        currentHealth += scEquipment.HealthAdd;
        MaxHealth += scEquipment.HealthAdd;
        Reach += scEquipment.ReachAdd;
        Speed += scEquipment.SpeedAdd;
        Regen += scEquipment.RegenAdd;
        Dodge += scEquipment.DodgeAdd;
        CritChance += scEquipment.CritChanceAdd;
        LifeSteal += scEquipment.LifeStealAdd;
        ResistDamage += scEquipment.ResistDamageAdd;

        HealthBar.SetHealth(currentHealth);
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (!photonView.IsMine) return;

        if (Random.Range(0, 100) <= Dodge)
        {
            var go = Instantiate(dodgeText, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().color = Color.white;
            go.GetComponent<TextMesh>().text = "DODGED";
            return;
        }

        damage -= ResistDamage;
        if (damage < 0) { damage = 0; }

        currentHealth -= damage;
        MainCamera.GetComponent<ScreenShake>().Shake(damage);
        SoundManager.Instance.PlaySound(playerHurt);
        HealthBar.SetHealth(currentHealth);
    }

    [PunRPC]
    public void IncreaseCoins(int n)
    {
        if (photonView.IsMine)
        {
            Coin += n;
            coinCounter.SetCoins(Coin);
        }
    }
}
