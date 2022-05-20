using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public ObstacleStats stats;

    private void Awake()
    {
        stats = GetComponent<ObstacleStats>();
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}