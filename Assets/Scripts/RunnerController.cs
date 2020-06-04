using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunnerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isJumped;
    private bool _isDashed;
    private bool _isDashOnCooldown;
    private float _horizontalSpeed;
    
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float airSpeed = 0.5f;
    [SerializeField] private float jumpSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 3.0f;
    [SerializeField] private float dashCooldownDuration = 5.0f;
    [SerializeField] private GameObject dashCooldownImage;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isJumped = false;
        _isDashed = false;
        _isDashOnCooldown = false;
        _horizontalSpeed = moveSpeed;
    }
    
    private void FixedUpdate()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && !_isJumped)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.W) && !_isDashed && !_isDashOnCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * _horizontalSpeed, _rigidbody.velocity.y, 0f);
    }

    private void Jump()
    {
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/Jump"));
        if (GameManager.IsGravityReversed)
        {
            _rigidbody.velocity += new Vector3(0f, -jumpSpeed, 0f);
        }
        else
        {
            _rigidbody.velocity += new Vector3(0f, jumpSpeed, 0f);
        }
        
        _horizontalSpeed = airSpeed;
        _isJumped = true;
    }

    private IEnumerator Dash()
    {
        StartCoroutine(SetDashCooldown());
        var originalScale = transform.localScale;
        var endPos = transform.position + new Vector3(2.0f, 0f, 0f);
        float duration = 0.1f;
        
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/Dash"));
        yield return AnimationManager.Scale(this.gameObject, originalScale, Vector3.zero, duration);
        transform.position = endPos;
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, 0f);
        yield return AnimationManager.Scale(this.gameObject, transform.localScale, originalScale, duration);
    }

    private IEnumerator SetDashCooldown()
    {
        _isDashOnCooldown = true;
        var startTime = Time.time;
        while (Time.time < startTime + dashCooldownDuration)
        {
            dashCooldownImage.GetComponent<Image>().fillAmount = (Time.time - startTime) / dashCooldownDuration;
            yield return null;
        }
        _isDashOnCooldown = false;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumped = false;
            _isDashed = false;
            _horizontalSpeed = moveSpeed;
        }
        
        if (other.gameObject.CompareTag("Death"))
        {
            GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/Hit_Hurt"));
            SceneManager.LoadScene("MainScene"); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/Hit_Hurt"));
            SceneManager.LoadScene("MainScene");
        }
    }
}
