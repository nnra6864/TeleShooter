using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyWave : ScriptableObject
{
    public float waveStartCountdown;
    public List<GameObject> enemies;
    public List<float> cooldowns;
}
