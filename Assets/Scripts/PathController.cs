using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PathController : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _endPos;
    
    // Start is called before the first frame update
    void Awake()
    {
        _startPos = transform.position;
        _endPos = new Vector3(-5f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        var startTime = Time.time;
        float duration = (_startPos.x - _endPos.x)/4;
        
        while (Time.time < startTime + duration)
        {
            transform.position = Vector3.Lerp(_startPos, _endPos, ((Time.time - startTime)/duration));
            yield return null;
        }

        transform.position = _endPos;
        Destroy(gameObject);
    }
}
