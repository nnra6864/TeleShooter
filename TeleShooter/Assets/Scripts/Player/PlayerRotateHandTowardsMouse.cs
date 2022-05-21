using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateHandTowardsMouse : MonoBehaviour
{
    [HideInInspector]
    public PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
    }

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        float angle = Mathf.Atan2(mouseWorldPos.y - transform.position.y, mouseWorldPos.x - transform.position.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (angle < -90 || angle > 90 && !playerStats.IsBackwards)
        {
            playerStats.IsBackwards = true;
            playerStats.gameObject.transform.rotation = Quaternion.Euler(-playerStats.gameObject.transform.rotation.x, playerStats.gameObject.transform.rotation.y, playerStats.transform.rotation.z);
        }
        else if (angle > -90 || angle < 90 && playerStats.IsBackwards)
        {
            playerStats.IsBackwards = false;
            playerStats.gameObject.transform.rotation = Quaternion.Euler(-playerStats.gameObject.transform.rotation.x, playerStats.gameObject.transform.rotation.y, playerStats.transform.rotation.z);
        }
    }
}