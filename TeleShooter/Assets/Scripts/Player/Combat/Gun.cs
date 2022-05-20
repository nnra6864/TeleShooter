using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Transform projectileSpawnPosition;

    //Bullets
    public List<GameObject> bullets = new List<GameObject>();
    [HideInInspector] public GameObject chosenBullet;
    public int bulletType; //Select and index from list
    public bool shouldUseRandomBullets;
    public bool chooseRandomBulletOnlyOnce;

    //Burst
    public bool isBurst;
    public int burstLength;
    public bool shouldChangeBulletEveryBurst;
    public float cooldownBetweenBurstBullets;

    //General
    [HideInInspector] public GameObject bullet;
    public float shootingCooldown;
    public bool canHold;
    public bool canShoot;
    [HideInInspector] public bool isShooting;
    public float playerKnockbackStrength;

    void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        projectileSpawnPosition = transform.Find("Projectile Spawn Position").transform;
    }

    void Update()
    {
        if (canShoot && !isShooting)
        {
            if (canHold)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    StartCoroutine(SpawnProjectile());
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    StartCoroutine(SpawnProjectile());
                }
            }
        }
    }

    public void ChooseRandomProjectile() 
    {
        if (shouldUseRandomBullets)
        {
            chosenBullet = bullets[Random.Range(0, bullets.Count)];
            if (chooseRandomBulletOnlyOnce)
            {
                shouldUseRandomBullets = false;
            }
        }
        else
        {
            if (bulletType < 0) { bulletType = 0; }
            chosenBullet = bullets[bulletType];
        }
    }

    public void InstantiateProjectile() 
    {
        Debug.Log("0)");
    }

    public IEnumerator SpawnProjectile() 
    {
        isShooting = true;

        ChooseRandomProjectile();

        if (isBurst)
        {
            if (shouldChangeBulletEveryBurst)
            {
                ChooseRandomProjectile();
            }

            InstantiateProjectile();

            yield return new WaitForSeconds(cooldownBetweenBurstBullets);
        }
        else
        {
            InstantiateProjectile();
        }

        yield return new WaitForSeconds(shootingCooldown);
        isShooting = false;
    }
}
