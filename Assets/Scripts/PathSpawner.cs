using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathSpawner : MonoBehaviour
{
    private bool _didWaitForSpawn;
    private float _waitForSpawnDuration;
    
    [SerializeField] private List<GameObject> pathPrefabs;
    [SerializeField] private float maxWaitForSpawnDuration = 1.0f;
    [SerializeField] private float minWaitForSpawnDuration = 0.75f;
    
    // Start is called before the first frame update
    void Start()
    {
        _didWaitForSpawn = true;
        _waitForSpawnDuration = maxWaitForSpawnDuration;
    }

    private void Update()
    {
        _waitForSpawnDuration -= Time.deltaTime/10;
        
        if (_waitForSpawnDuration < minWaitForSpawnDuration)
        {
            _waitForSpawnDuration = minWaitForSpawnDuration;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_didWaitForSpawn)
        {
            SpawnPath();
        }
    }

    private void SpawnPath()
    {
        var path = Instantiate(pathPrefabs[Random.Range(0,pathPrefabs.Count)], transform.position, pathPrefabs[Random.Range(0,pathPrefabs.Count)].transform.rotation);
        StartCoroutine(WaitForSpawn());
    }

    private IEnumerator WaitForSpawn()
    {
        _didWaitForSpawn = false;
        yield return new WaitForSeconds(_waitForSpawnDuration);
        _didWaitForSpawn = true;
    }
}
