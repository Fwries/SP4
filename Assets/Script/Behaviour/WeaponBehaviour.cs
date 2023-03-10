using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private AudioClip swordSwing;
    [SerializeField] private AudioClip weaponEquip;

    public GameObject Weapon;

    private Vector3 dir;
    private float angle;
    private bool IsCoolDown;
    private float Cooldown;

    private bool IsAttack;
    private float AtkSwingZ;
    private float AtkSwingY;
    private float StartSwing;
    private float EndSwing;
    private int atkType;

    private Vector3 TransformPos;

    public ScWeapon scWeapon;
    public Hitbox[] hitBoxes;
    public CoolDownBehavior cooldownBar;
    private PhotonView photonview;

    void Start()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        cooldownBar = GameObject.Find("CooldownBar").GetComponent<CoolDownBehavior>();
    }
    void Awake()
    {
        photonview = this.transform.parent.GetComponent<PhotonView>();
        WeaponSwitch(scWeapon);
    }

    private void Update()
    {
        if (photonview.IsMine)
        {
            Inventory.instance.SetWeapon(scWeapon);
            if (Weapon == null) { return; }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask) && IsAttack == false)
            {
                //transform.position = raycastHit.point;
                dir = new Vector3(raycastHit.point.x - transform.position.x,
                                  0.0f,
                                  raycastHit.point.z - transform.position.z);
            }

            if (Input.GetButtonDown("Fire1") && !IsAttack && !IsCoolDown)
            {
                if (atkType == 1) // Swing
                {
                    Weapon.transform.Rotate(90, 0, 0);
                    AtkSwingY = StartSwing;
                    IsAttack = true;
                    foreach (Hitbox hitbox in hitBoxes)
                        hitbox.active = true;
                }
                else if (atkType == 2) // Stab
                {
                    Weapon.transform.Rotate(0, -90, 0);
                    Weapon.transform.position -= (Weapon.transform.forward) * -0.2f;
                    Weapon.transform.Rotate(0, 90, 0);
                    IsAttack = true;

                    foreach (Hitbox hitbox in hitBoxes)
                        hitbox.active = true;
                }
                else if (atkType == 3) // Crush
                {
                    AtkSwingZ = StartSwing;
                    IsAttack = true;
                    foreach (Hitbox hitbox in hitBoxes)
                        hitbox.active = true;
                }

                if (Random.Range(0, 100) <= playerStats.CritChance)
                {
                    foreach (Hitbox hitbox in hitBoxes)
                        hitbox.Crit = 2;
                }
                else
                {
                    foreach (Hitbox hitbox in hitBoxes)
                        hitbox.Crit = 1;
                }
                SoundManager.Instance.PlaySound(swordSwing);
            }

            if (IsAttack)
            {
                if (atkType == 1) // Swing
                {
                    AtkSwingY += 720 * Time.deltaTime / (scWeapon.AtkSpeed + playerStats.AtkSpeed);
                    if (AtkSwingY > EndSwing)
                    {
                        Weapon.transform.Rotate(-90, 0, 0);
                        AtkSwingY = 0;
                        IsAttack = false;
                        IsCoolDown = true;

                        foreach (Hitbox hitbox in hitBoxes)
                            hitbox.active = false;
                    }
                }
                else if (atkType == 2) // Stab
                {
                    Weapon.transform.Rotate(0, -90, 0);
                    EndSwing += Time.deltaTime * (scWeapon.AtkSpeed + playerStats.AtkSpeed);
                    Weapon.transform.position -= (Weapon.transform.forward) * EndSwing * (scWeapon.AtkReach + playerStats.Reach);
                    Weapon.transform.Rotate(0, 90, 0);

                    if (EndSwing > ((scWeapon.AtkSpeed + playerStats.AtkSpeed) / 10))
                    {
                        Weapon.transform.position = new Vector3(transform.position.x + scWeapon.OffsetX, transform.position.y + scWeapon.OffsetY, transform.position.z);
                        EndSwing = 0;
                        IsAttack = false;
                        IsCoolDown = true;

                        foreach (Hitbox hitbox in hitBoxes)
                            hitbox.active = false;
                    }
                }
                else if (atkType == 3) // Crush
                {
                    AtkSwingZ += 720 * Time.deltaTime / (scWeapon.AtkSpeed + playerStats.AtkSpeed);
                    if (AtkSwingZ > EndSwing)
                    {
                        AtkSwingZ = 0;
                        IsAttack = false;
                        IsCoolDown = true;

                        foreach (Hitbox hitbox in hitBoxes)
                            hitbox.active = false;
                    }
                }
            }
            else
            {
                angle = -Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            }

            if (IsCoolDown)
            {
                Cooldown += Time.deltaTime;
                cooldownBar.SetCooldown(Cooldown);
                if (Cooldown >= scWeapon.AtkCooldown)
                {
                    IsCoolDown = false;
                    Cooldown = 0;
                    if (scWeapon.Name == "Stick")
                    {
                        atkType = Random.Range(1, 4);
                    }
                }
            }

            transform.rotation = Quaternion.Euler(0.0f, angle + AtkSwingY, -AtkSwingZ);

            if (Input.GetButtonDown("Fire2") && !IsAttack && !IsCoolDown)
            {
                foreach (Hitbox hitbox in hitBoxes)
                    hitbox.active = true;

                Weapon.GetComponent<ThrowWeapon>().Throw(dir, angle, scWeapon, hitBoxes);
                GetComponent<PhotonView>().RPC("RPC_ThrowWeapon", RpcTarget.All, this.GetComponent<PhotonView>().ViewID, Weapon.GetComponent<PhotonView>().ViewID);
            }
        }
    }

    public void Attack(int Damage)
    {
        MainCamera.GetComponent<ScreenShake>().Shake(Damage);
        playerStats.RecoverHealth(playerStats.LifeSteal);
        
        if (scWeapon.Name == "Scythe")
        {
            playerStats.RecoverHealth(3);
        }
    }

    public void WeaponSwitch(ScWeapon _scWeapon)
    {
        scWeapon = _scWeapon;

        if (photonview.IsMine)
        {
            if (Weapon != null) { Destroy(Weapon); }
            
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            Weapon = PhotonNetwork.Instantiate(scWeapon.Prefab.name, new Vector3(transform.position.x + scWeapon.OffsetX, transform.position.y + scWeapon.OffsetY, transform.position.z), new Quaternion(0, 0, 0, 0), 0);
            Weapon.transform.SetParent(transform);
            Weapon.GetComponent<MeshCollider>().enabled = false;
            hitBoxes = Weapon.GetComponent<HitboxContainer>().hitboxes;

            GetComponent<PhotonView>().RPC("RPC_SetWeapon", RpcTarget.Others, this.GetComponent<PhotonView>().ViewID, Weapon.GetComponent<PhotonView>().ViewID);
        }
        atkType = (int)scWeapon.AtkType;

        if (atkType == 1) // Swing
        {
            EndSwing = scWeapon.AtkRange / 4;
            StartSwing = EndSwing * -3;
        }
        else if (atkType == 3) // Crush
        {
            EndSwing = scWeapon.AtkRange / 4;
            StartSwing = EndSwing * -3;
        }
        SoundManager.Instance.PlaySound(weaponEquip);
    }

    [PunRPC]
    void RPC_SetWeapon(int view_id, int Weapon_id)
    {
        GameObject Parent = PhotonView.Find(view_id).gameObject;
        WeaponBehaviour ParentBehav = Parent.GetComponent<WeaponBehaviour>();
        ParentBehav.Weapon = PhotonView.Find(Weapon_id).gameObject;
        ParentBehav.Weapon.transform.SetParent(Parent.transform);
        ParentBehav.Weapon.GetComponent<MeshCollider>().enabled = false;
    }

    [PunRPC]
    void RPC_ThrowWeapon(int view_id, int Weapon_id)
    {
        GameObject ThisGameObject = PhotonView.Find(view_id).gameObject;
        WeaponBehaviour WeaponBehav = ThisGameObject.GetComponent<WeaponBehaviour>();

        WeaponBehav.Weapon.transform.SetParent(null);
        WeaponBehav.Weapon = null;
        WeaponBehav.scWeapon = null;
        SoundManager.Instance.PlaySound(swordSwing);
        ThisGameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }
}
