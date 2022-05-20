using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OnSceneLoad : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    private GameObject player;
    private int randomSpawnPointIndex;
    private List<GameObject> spawnPoints = new List<GameObject>();

    private GameObject mainVirtualCamera;

    void Start()
    {
        spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
        player = Instantiate(playerPrefab, spawnPoints[randomSpawnPointIndex].transform.position, new Quaternion(0, 0, 0, 0));
        player.name = player.name.Replace("(Clone)", "");

        mainVirtualCamera = GameObject.Find("Main Virtual Camera");
        mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 5;
        mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }
}
