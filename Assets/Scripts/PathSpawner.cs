using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    private bool _didWaitForSpawn;
    private GameObject _pathPrefab;

    [SerializeField] private float waitForSpawnDuration = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _didWaitForSpawn = true;
        _pathPrefab = Resources.Load<GameObject>("Prefabs/Path");
    }

    // Update is called once per frame
    void Update()
    {
        if (_didWaitForSpawn)
        {
            SpawnPath();
        }
    }

    private void SpawnPath()
    {
        var path = Instantiate(_pathPrefab, transform.position, _pathPrefab.transform.rotation);
        StartCoroutine(WaitForSpawn());
    }

    private IEnumerator WaitForSpawn()
    {
        _didWaitForSpawn = false;
        yield return new WaitForSeconds(waitForSpawnDuration);
        _didWaitForSpawn = true;
    }
}
