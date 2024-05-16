using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpprueba : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] float _jumpHeight = 1;

    float _gravity = -9.81f;
    Vector3 _playerGravity;

    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private float _sensorRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
      Jump();  
    }

    void Jump()
    {
        _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);
        //_animator.SetBool("IsJumping", !_isGrounded);

        if(_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = -2;
        }
        if(_isGrounded && Input.GetButtonDown("Jump"))
        {
            _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
            //_animator.SetBool("IsJumping", true);
        }        
        _playerGravity.y += _gravity * Time.deltaTime;
        
        _controller.Move(_playerGravity * Time.deltaTime);
    }
}
