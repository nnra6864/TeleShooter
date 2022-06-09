using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class CustomGravity : MonoBehaviour
{
    Stats stats;
    Clock clock;
    Rigidbody rigidBody;

    [Tooltip("Name of the Chronos Clock you'd like to use.\nLeave empty for default values.")]
    [SerializeField]string clockName;

    [Tooltip("Ammount of gravity that will be added.")]
    [SerializeField] float gravity;

    [Tooltip("Gravity with all modifiers applied.")]
    [ReadOnlyField] public float modifiedGravity;

    void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody>();

        if (this.CompareTag("Player"))
        {
            stats = this.GetComponent<PlayerStats>();
            clockName = "Player";
        }
        else if (this.CompareTag("Enemy"))
        {
            stats = this.GetComponent<EnemyStats>();
            clockName = "Enemy";
        }
        else 
        {
            stats = this.GetComponent<Stats>();
            clockName = "Enemy";
        }

        this.enabled = stats == null ? false : true;
        rigidBody.useGravity = !stats.useCustomGravity;
        this.enabled = stats.useCustomGravity;
    }

    private void Update()
    {
        clock = Timekeeper.instance.Clock(clockName);
        modifiedGravity = gravity * clock.timeScale * stats.gravityMultiplier;
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(new Vector3(0, -modifiedGravity, 0));
        
        if (!stats.useFallMultiplier || rigidBody.velocity.y >= 0) return;
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y + (rigidBody.velocity.y / stats.fallMultiplier), rigidBody.velocity.z);
    }
}
