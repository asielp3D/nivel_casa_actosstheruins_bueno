using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSControllerJac : MonoBehaviour
{
    private CharacterController _controller;
    private Transform _camera;
    private float _horizontal;
    private float _vertical;
    [SerializeField] private float _playerSpeed = 5;
    [SerializeField] private float _jumpHeight = 1;

    [SerializeField] private GameObject HeadPosition;
    [SerializeField] private bool _crouch = false;
    [SerializeField] private bool _canStand;

    private float _gravity = -9.81f;
    private Vector3 _playerGravity;

    private float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f;

    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private float _sensorRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;
    private Animator _animator;

    //Escalada
    public float velocidadEscalada = 5f;
    public float distanciaMaxima = 2f;
    public Transform puntoInicioEscalada;

    private bool escalando = false;
    private Vector3 puntoFinalEscalada;

    //Disparo
    /*[SerializeField] Transform gunPosition;
    [SerializeField] int ammo;
    public GameObject bullet;*/
    
 void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        Movement();
        Jump();
        Crouch();
        if (escalando)
        {
            float movimientoVertical = Input.GetAxis("Vertical");
            Vector3 movimiento = new Vector3(0, movimientoVertical, 0) * Time.deltaTime * velocidadEscalada;
            transform.Translate(movimiento);
            if (Vector3.Distance(transform.position, puntoFinalEscalada) >= distanciaMaxima)
            {
                FinalizarEscalada();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ComenzarEscalada();
            }
        }
        //Disparo
        /*if (Input.GetMouseButtonDown(0))
        {
            if(ammo > 0)
            {
                Instantiate(bullet, gunPosition.position, gunPosition.rotation);
                ammo = ammo -1;
            }
        }*/
    }
    void ComenzarEscalada()
    {
        escalando = true;
        puntoFinalEscalada = puntoInicioEscalada.position + Vector3.up * distanciaMaxima; 
    }
    
    void FinalizarEscalada()
    {
        escalando = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Escalable"))
        {
            puntoInicioEscalada = other.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Escalable"))
        {
            FinalizarEscalada();
        }
    }
    
    void Movement()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);
        _animator.SetFloat("VelX", 0);
        _animator.SetFloat("VelZ", direction.magnitude);

        if(direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
            Vector3 moveDirection = Quaternion.Euler(0,targetAngle, 0) * Vector3.forward;
            _controller.Move(moveDirection.normalized * _playerSpeed * Time.deltaTime);
        }
        //Ladder no funciona
        /*float avoidFloorDistance = .1f;
        float ladderGrabDistance = .4f;
        if (Physics.Raycast(transform.position + Vector3.up * avoidFloorDistance, direction, out RaycastHit raycastHit, ladderGrabDistance))
        {
            if(raycastHit.transform.TryGetComponent(out Ladder ladder))
            {
                direction.x = 0f;
                direction.y = direction.z;
                direction.z = 0f;
                _isGrounded = true;
                

            }
            Debug.Log(raycastHit.transform);
        }*/
    }
    void Crouch()
    {
        if (Physics.Raycast(HeadPosition.transform.position, Vector3.up, 0.5f))
        {
            _canStand = false;
            Debug.DrawRay(HeadPosition.transform.position, Vector3.up, Color.red);
        }
        else
        {
            _canStand = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(_crouch == true && _canStand == true)
            {
                _crouch = false;
                _animator.SetBool("IsCrouching", false);
                _jumpHeight = 1;
                _controller.height = 2f;
                _playerSpeed = 5;
                _controller.center = new Vector3(0f, 0f, 0f);
                
            }
            else
            {
                _crouch = true;
                _animator.SetBool("IsCrouching", true);
                _jumpHeight = 0;
                _controller.height = 1.5f;
                _playerSpeed = 2;
                _controller.center = new Vector3(0f, -0.2f, 0f);
            }
        }
    }

    void Jump()
    {
        _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);
        _animator.SetBool("IsJumping", !_isGrounded);

        if(_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = -2;
        }
        if(_isGrounded && Input.GetButtonDown("Jump"))
        {
            _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }
        _playerGravity.y += _gravity * Time.deltaTime;
        _controller.Move(_playerGravity * Time.deltaTime);
    }
}
