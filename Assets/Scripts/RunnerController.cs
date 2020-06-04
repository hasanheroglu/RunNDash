using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isJumped;
    private bool _isDashed;
    private float _horizontalSpeed;
    
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float airSpeed = 0.5f;
    [SerializeField] private float jumpSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 3.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isJumped = false;
        _isDashed = false;
        _horizontalSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && !_isJumped)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.W) && !_isDashed)
        {
            StartCoroutine(DashTest(transform.position + new Vector3(2f, 0f, 0f)));
            //Dash();
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

    private IEnumerator DashTest(Vector3 endPos)
    {
        var startTime = Time.time;
        var startScale = new Vector3(0.25f, 0.25f, 0.25f);
        var endScale = Vector3.zero;
        float duration = 0.1f;
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/Dash"));

        while (Time.time < startTime + duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, ((Time.time - startTime)/duration));
            yield return null;
        }

        transform.localScale = endScale;
        
        transform.position = endPos;
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, 0f);
        
        startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            transform.localScale = Vector3.Lerp(endScale, startScale, ((Time.time - startTime)/duration));
            yield return null;
        }
        
        transform.localScale = startScale;
    }

    private void Dash()
    {
        var dashVelocity = new Vector3(dashSpeed, 0f, 0f);
        _rigidbody.velocity = dashVelocity;
        _isDashed = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumped = false;
            _isDashed = false;
            _horizontalSpeed = moveSpeed;
        }
        
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/Hit_Hurt"));
            SceneManager.LoadScene("SampleScene"); 
        }
    }
}
