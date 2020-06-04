using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float _score;
    private bool _isGamePaused;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject scoreText;
    
    public static bool IsGravityReversed;

    
    // Start is called before the first frame update
    void Start()
    {
        IsGravityReversed = false;
        _score = 0f;
        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isGamePaused)
        {
            _score += Time.deltaTime * 10;
            scoreText.GetComponent<TextMeshProUGUI>().text = String.Format("{0:D6}", (int) _score);
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
        else
        {
            if (Input.anyKey)
            {
                Resume();
            }
        }
    }

    private void Pause()
    {
        _isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        _isGamePaused = false;
        pauseMenu.SetActive(false);
    }

    private void ReverseGravity()
    {
        Physics.gravity *= -1;
        IsGravityReversed = !IsGravityReversed;
    }
}
