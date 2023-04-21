// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviourPun
{
    public GameObject[] items;

    public float maxDistance = 40f;

    public float timeBetSpawnMax = 60f;
    public float timeBetSpawnMin = 30f;
    public float timeBetSpawn;
    
    public float lastSpawnTime;

    void Start()
    {
        maxDistance = 40f;
        timeBetSpawn = Random.Range(timeBetSpawn, timeBetSpawnMax);
        lastSpawnTime = 0;
    }
    
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            Spawn();
        }
    }

    private void Spawn()
    {
        Vector3 spawnPosition = GetRandomPointOnNavMesh(new Vector3(84.7f, 23, 56f), maxDistance);
        spawnPosition += Vector3.up * 0.5f;

        GameObject selectedItem = items[Random.Range(0, items.Length)];

        GameObject item = PhotonNetwork.Instantiate(selectedItem.name, spawnPosition, Quaternion.Euler(-90f, 0, -25.038f));

        StartCoroutine(DestroyAfter(item, 60f));
    }

    IEnumerator DestroyAfter(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (target != null)
        {
            PhotonNetwork.Destroy(target);
        }
    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}
