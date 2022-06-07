using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    EnemyStats enemyStats;

    [Tooltip("Must have the exact same name as tag you'd like to filter.")]
    public List<string> ignoreTags;

    [Tooltip("Must have the exact same name as layer you'd like to filter.")]
    public List<string> ignoreLayers;

    private void Awake()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var tag in ignoreTags) { if (other.CompareTag(tag)) return; }
        foreach (var layer in ignoreLayers) { if (other.gameObject.layer == LayerMask.NameToLayer(layer)) return; }

        enemyStats.isGrounded = true;
        enemyStats.jumpsLeft = enemyStats.numberOfJumps;
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (var tag in ignoreTags) { if (other.CompareTag(tag)) return; }
        foreach (var layer in ignoreLayers) { if (other.gameObject.layer == LayerMask.NameToLayer(layer)) return; }

        enemyStats.isGrounded = true;
        enemyStats.jumpsLeft = enemyStats.numberOfJumps;
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var tag in ignoreTags) { if (other.CompareTag(tag)) return; }
        foreach (var layer in ignoreLayers) { if (other.gameObject.layer == LayerMask.NameToLayer(layer)) return; }

        enemyStats.isGrounded = false;
    }
}
