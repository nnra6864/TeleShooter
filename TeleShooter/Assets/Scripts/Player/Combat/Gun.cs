using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Variables
    PlayerStats playerStats;
    Rigidbody playerRigidbody;
    Transform projectileSpawnPosition; //When creating a Gun, create a new Empty Game Object as Gun's Child and place it at the tip of your gun and name it Projectile Spawn Position


    [Header("Projectile")]

    [Tooltip("List of prefabs you'd like to use as projectiles.")]
    public List<GameObject> projectiles = new List<GameObject>();

    [Tooltip("If you are not using random projectiles, projectile with this index will be selected from the list.")]
    public int projectileType;

    [Tooltip("Choose a random projectile from the list every time you shoot.")]
    public bool randomProjectiles;

    [Tooltip("Chooses random projectile only the first time a gun fires.")]
    public bool chooseRandomProjectileOnlyOnce;

    [HideInInspector]
    public GameObject chosenProjectile;


    [Header("Burst")]

    [Tooltip("Enable if you'd like your gun to shoot in bursts.")]
    public bool isBurst;

    [Tooltip("Number of bullets per burst.")]
    public int burstLength;

    [Tooltip("Time that passes between each bullet in a burst.")]
    public float cooldownBetweenBurstProjectiles;

    [Tooltip("Enable if you'd like every projectile in a single burst to be different.\nFor getting bursts of the same random bullet enable random projectiles and disable this.")]
    public bool randomBurstProjectiles;

    
    [Header("General")]

    [Tooltip("Determines if you can hold to shoot.")]
    public bool canHold;

    [Tooltip("If disabled, player won't be able to shoot.")]
    public bool canShoot;

    [Tooltip("Time that has to pass before first Bullet is Fired.")]
    public float timeBeforeFirstBulletIsFired;

    [Tooltip("Time that passes before you can shoot again.")]
    public float shootingCooldown;

    [Tooltip("Enable if you'd like a player to recieve knockback when shooting.")]
    public bool shouldKnockBackPlayer;

    [Tooltip("Multiplies the strength of knockback\n(knockback * multiplier).")]
    public float playerKnockbackMultiplier;

    [HideInInspector]
    public GameObject projectile;

    [HideInInspector]
    public bool isShooting;
    #endregion

    void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        playerRigidbody = GetComponentInParent<Rigidbody>();
        projectileSpawnPosition = transform.Find("Projectile Spawn Position").transform;
    }

    void Update()
    {
        if (canShoot && playerStats.canShoot && !isShooting)
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
        if (randomProjectiles)
        {
            chosenProjectile = projectiles[Random.Range(0, projectiles.Count)];
            if (chooseRandomProjectileOnlyOnce)
            {
                randomProjectiles = false;
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

        var projectileStats = projectile.GetComponent<ProjectileStats>();
        projectileStats.shotByPlayer = true;
        projectileStats.damageMultiplier = playerStats.damageMultiplier;

        playerStats.totalBulletsFired++;
    }

    public IEnumerator SpawnProjectile() 
    {
        isShooting = true;

        ChooseRandomProjectile();

        yield return new WaitForSeconds(timeBeforeFirstBulletIsFired);

        if (isBurst)
        {
            for (int i = 0; i < burstLength; i++)
            {
                if (randomBurstProjectiles)
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
