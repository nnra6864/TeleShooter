using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Transform projectileSpawnPosition;

    //Projectiles
    [Tooltip("List of prefabs you'd like to use as projectiles.")]
    public List<GameObject> projectiles = new List<GameObject>();

    [HideInInspector] public GameObject chosenProjectile;

    [Tooltip("If you are not using random projectiles, projectile with this index will be selected from the list.")]
    public int projectileType;

    [Tooltip("Choose a random projectile from the list every time you shoot.")]
    public bool shouldUseRandomProjectiles;

    [Tooltip("Chooses random projectile only the first time a gun fires.")]
    public bool chooseRandomProjectileOnlyOnce;

    //Burst
    [Tooltip("Enable if you'd like your gun to shoot in bursts.")]
    public bool isBurst;

    [Tooltip("Number of bullets per burst.")]
    public int burstLength;

    [Tooltip("Time that passes between each bullet in a burst.")]
    public float cooldownBetweenBurstProjectiles;

    [Tooltip("Enable if you'd like every projectile in a single burst to be different.\nFor getting bursts of the same random bullet enable random projectiles and disable this.")]
    public bool shouldChangeProjectileEveryBurst;

    //General
    [HideInInspector] public GameObject projectile;

    [Tooltip("Time that passes before you can shoot again.")]
    public float shootingCooldown;

    [Tooltip("Determines if you can hold to shoot.")]
    public bool canHold;

    [Tooltip("If disabled, player won't be able to shoot.")]
    public bool canShoot;
    [HideInInspector] public bool isShooting;

    [Tooltip("Enable if you'd like a player to recieve knockback when shooting.")]
    public bool shouldKnockBackPlayer;

    [Tooltip("Multiplies the strength of knockback\n(knockback * multiplier).")]
    public float playerKnockbackMultiplier;

    void Awake()
    {
        playerRigidbody = GetComponentInParent<Rigidbody>();
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
        if (shouldUseRandomProjectiles)
        {
            chosenProjectile = projectiles[Random.Range(0, projectiles.Count)];
            if (chooseRandomProjectileOnlyOnce)
            {
                shouldUseRandomProjectiles = false;
            }
        }
        else
        {
            if (projectileType < 0) { projectileType = 0; }
            chosenProjectile = projectiles[projectileType];
        }
    }

    public void InstantiateProjectile() 
    {
        projectile = Instantiate(chosenProjectile, projectileSpawnPosition.position, projectileSpawnPosition.rotation, null);
        projectile.name = chosenProjectile.name.Replace("(Clone)", "");
    }

    public IEnumerator SpawnProjectile() 
    {
        isShooting = true;

        ChooseRandomProjectile();

        if (isBurst)
        {
            for (int i = 0; i < burstLength; i++)
            {
                if (shouldChangeProjectileEveryBurst)
                {
                    ChooseRandomProjectile();
                }

                InstantiateProjectile();
                KnochBackPlayer();

                yield return new WaitForSeconds(cooldownBetweenBurstProjectiles);
            }
        }
        else
        {
            InstantiateProjectile();
            KnochBackPlayer();
        }

        yield return new WaitForSeconds(shootingCooldown);
        isShooting = false;
    }

    public void KnochBackPlayer() 
    {
        if (shouldKnockBackPlayer && playerRigidbody != null)
        {
            playerRigidbody.AddForce(-transform.right * playerKnockbackMultiplier, ForceMode.Force);
        }
    }
}
