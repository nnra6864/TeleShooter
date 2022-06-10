using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    [Tooltip("Add EnemyWave scriptable objects in this list.")]
    public List<EnemyWave> enemyWaves;
    List<float> waveLength;
    [ReadOnlyField] public int enemyWaveIndex;

    List<GameObject> spawnPoints;

    [Tooltip("Determines if random Spawn Point will be chosen for each round.")]
    public bool chooseRandomSpawnPointEachRound;

    [Tooltip("Determines if random Spawn Point will be chosen for each wave.")]
    public bool chooseRandomSpawnPointEachWave;

    [Tooltip("Determines if random Spawn Point will be chosen for each enemy.")]
    public bool chooseRandomSpawnPointEachEnemy;

    [Tooltip("Index of a SpawnPoint enemies will be spawned by.\nChoose Random Spawn Point has to be disabled in order for this to work.")]
    public int spawnPointIndex;

    void Awake()
    {
        enemyWaveIndex = -1;

        //Add length of each wave to the list
        waveLength = new List<float>();
        foreach (EnemyWave enemyWave in enemyWaves) 
        {
            float wl = enemyWave.waveStartCountdown;
            foreach (float cd in enemyWave.cooldowns) 
            {
                wl += cd;
            }
            waveLength.Add(wl);
        }

        //Add all spawnpoints to the list
        spawnPoints = new List<GameObject>();
        foreach (Transform child in transform) 
        {
            spawnPoints.Add(child.gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(StartEnemyWave());
    }

    IEnumerator StartEnemyWave() 
    {
        spawnPointIndex = chooseRandomSpawnPointEachRound ? Random.Range(0, spawnPoints.Count) : spawnPointIndex;

        foreach (EnemyWave enemyWave in enemyWaves) 
        {
            enemyWaveIndex++;
            StartCoroutine(CountDown());
            StartCoroutine(SpawnEnemyWave(enemyWave));
            yield return new WaitForSeconds(waveLength[enemyWaveIndex]);
            StopCoroutine(CountDown());
        }
    }

    IEnumerator SpawnEnemyWave(EnemyWave enemyWave)
    {
        int ewIndex = 0;
        yield return new WaitForSeconds(enemyWave.waveStartCountdown);

        spawnPointIndex = chooseRandomSpawnPointEachWave ? Random.Range(0, spawnPoints.Count) : spawnPointIndex;
        foreach (GameObject enemy in enemyWave.enemies)
        {
            spawnPointIndex = chooseRandomSpawnPointEachEnemy ? Random.Range(0, spawnPoints.Count) : spawnPointIndex;

            InstantiateEnemy(enemy);
            yield return new WaitForSeconds(enemyWave.cooldowns[ewIndex]);
            ewIndex++;
        }
    }

    IEnumerator CountDown() 
    {
        for (float i = waveLength[enemyWaveIndex]; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            waveLength[enemyWaveIndex]--;
            waveLength[enemyWaveIndex] = waveLength[enemyWaveIndex] < 0 ? 0 : waveLength[enemyWaveIndex];
        }
    }

    void InstantiateEnemy(GameObject enemy)
    {
        GameObject en = Instantiate(enemy, spawnPoints[spawnPointIndex].transform.position, enemy.transform.rotation);
        en.name = en.name.Replace("(Clone)", "");
    }
}