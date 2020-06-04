using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    private bool _didWaitForSpawn;
    
    [SerializeField] private List<GameObject> pathPrefabs;
    [SerializeField] private float waitForSpawnDuration = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _didWaitForSpawn = true;
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
        yield return new WaitForSeconds(waitForSpawnDuration);
        _didWaitForSpawn = true;
    }
}
