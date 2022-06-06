using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public List<GameObject> enemiesDifficultyOne = new List<GameObject>();
    public List<GameObject> enemiesDifficultyTwo = new List<GameObject>();
    public List<GameObject> enemiesDifficultyThree = new List<GameObject>();
    [HideInInspector] public List<GameObject> chosenEnemyDifficulty = new List<GameObject>();

    public float spawnCooldownLowerLimit;
    public float spawnCooldownUpperLimit;
    public int difficultyOneSpawnChance = 100;
    public int difficultyTwoSpawnChance = 0;
    public int difficultyThreeSpawnChance = 0;
    public int latestEnemyDifficulty;
    [HideInInspector] public bool isSpawning = false;
    public bool shouldSpawn = true;

    void Start()
    {

    }

    void Update()
    {
        if (shouldSpawn && !isSpawning)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    public void ChooseEnemyDifficulty() 
    {
        int total = difficultyOneSpawnChance + difficultyTwoSpawnChance + difficultyThreeSpawnChance;
        int rand = Random.Range(0, total);

        if (rand < difficultyOneSpawnChance)
        {
            latestEnemyDifficulty = 1;
        }
        else if (rand < difficultyTwoSpawnChance + difficultyOneSpawnChance)
        {
            latestEnemyDifficulty = 2;
        }
        else if(rand < difficultyThreeSpawnChance + difficultyOneSpawnChance + difficultyTwoSpawnChance)
        {
            latestEnemyDifficulty = 3;
        }
    }

    public void InstantiateEnemy() 
    {
        ChooseEnemyDifficulty();

        switch (latestEnemyDifficulty) 
        {
            // i'd suggest making this all integers so you dont have this mess
            case 1:
                chosenEnemyDifficulty = enemiesDifficultyOne;
                break;
            case 2:
                chosenEnemyDifficulty = enemiesDifficultyTwo;
                break;
            case 3:
                chosenEnemyDifficulty = enemiesDifficultyThree;
                break;
        }

        int rand = Random.Range(0, chosenEnemyDifficulty.Count);
        GameObject spawnedEnemy = Instantiate(chosenEnemyDifficulty[rand], transform.position, transform.rotation);
        spawnedEnemy.name = spawnedEnemy.name.Replace("(Clone)", "");
    }

    public IEnumerator SpawnEnemy() 
    {
        isSpawning = true;
        InstantiateEnemy();
        float cd = Random.Range(spawnCooldownLowerLimit, spawnCooldownUpperLimit);
        yield return new WaitForSeconds(cd);
        isSpawning = false;
    }
}
