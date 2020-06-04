using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isJumped;
    private bool _isDashed;

    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float jumpSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 3.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isJumped = false;
        _isDashed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!_isJumped)
        {
            Move();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !_isJumped)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.W) && !_isDashed)
        {
            Dash();
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f) * moveSpeed;
    }

    private void Jump()
    {
        var jumpVelocity = _rigidbody.velocity + new Vector3(0f, jumpSpeed, 0f);
        _rigidbody.velocity = jumpVelocity;
        _isJumped = true;
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
        }
    }
}
