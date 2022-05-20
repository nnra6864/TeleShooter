using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRoom : MonoBehaviour
{
    public int room;
    public int enemiesInRoom;
    public int playersInRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerStats>().CurrentRoom = room;
            playersInRoom++;
        }
        else if (other.tag == "Enemy") 
        {
            other.GetComponent<EnemyMove>().ChangeRoom(room);
            enemiesInRoom++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playersInRoom--;
        }
        else if (other.tag == "Enemy")
        {
            enemiesInRoom--;
        }       
    }
}
