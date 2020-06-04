using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool IsGravityReversed;
    
    // Start is called before the first frame update
    void Start()
    {
        IsGravityReversed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ReverseGravity();
        }
    }

    private void ReverseGravity()
    {
        Physics.gravity *= -1;
        IsGravityReversed = !IsGravityReversed;
    }
}
