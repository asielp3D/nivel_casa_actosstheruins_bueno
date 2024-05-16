using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SoldadoIA : MonoBehaviour
{
    public enum State
    {
        Patrolling,
        Chasing,
        Searching,
        Attacking,
        Distracted
    }
    
    public State currentState;

    //Patrullar
    private NavMeshAgent agent;
    public GameObject[] player;
    [SerializeField] private Transform[] patrolPoints;
    
    [SerializeField] float visionRange = 10;
    [SerializeField] float visionAngle = 90;
    
    [SerializeField] private float detectionRange = 15;
    

    //Rango de ataque
    [SerializeField] private float attackRange = 2;

    [SerializeField] float searchTimer;
    [SerializeField] float searchWaitTime = 15;
    [SerializeField] float searchRadius = 30;

    Vector3 lastTargetPosition;
    Animator anim;

    //Damage
    public TPSController personaje;
    public int takeDamage = 50;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectsWithTag("Player");
        anim = GetComponentInChildren<Animator>();
    }
    
    void Start()
    {
        SetRandomPoint();
        currentState = State.Patrolling;
    }
    
    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
            break;
            case State.Chasing:
                Chase();
            break;
            case State.Attacking:
                Attack();
            break;
            case State.Searching:
                Search();
            break;
            case State.Distracted:
                Distracted();
            break;
        }
        
        anim.SetFloat("VelX", 0);
        anim.SetFloat("VelZ", agent.velocity.magnitude);
    }

    void Patrol()
    {
        if(IsInRange() == true)
        {
            currentState = State.Chasing;
        }

        if(agent.remainingDistance < 0.5f)
        {
            SetRandomPoint();
        }
    }

    void Chase()
    {
        if(IsInRange() == false)
        {
            SetRandomPoint();
            currentState = State.Patrolling;
        }
        if(IsInRangeAttack() == true)
        {
            currentState = State.Attacking;
        }
        agent.destination = player[0].transform.position;
    }

    void Attack()
    {
        Debug.Log("Atacando");
        
        if(IsInRange() == false)
        {
            SetRandomPoint();
            currentState = State.Chasing;
        }
        if(IsInRangeAttack() == true)
        {
           personaje.TakeDamage(takeDamage);
        }
        
        SceneManager.LoadScene("Death");
    }

    void SetRandomPoint()
    {
        agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
    }

    void Search()
    {
        if(IsInRange() == true)
        {
            searchTimer = 0;
            currentState = State.Chasing;
        }
        searchTimer += Time.deltaTime;
        if(searchTimer < searchWaitTime)
        {
            if(agent.remainingDistance < 0.5f)
            {
                Debug.Log("Buscando punto aleatorio");
                Vector3 randomSearchPoint = lastTargetPosition + Random.insideUnitSphere * searchRadius;
                randomSearchPoint.y = lastTargetPosition.y;
                agent.destination = randomSearchPoint; 
            }
            
        }
        else
        {
            currentState = State.Patrolling;
        }
    }

    bool IsInRange()
    {
        Vector3 directionToPlayer = player[0].transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if(distanceToPlayer <= visionRange && angleToPlayer < visionAngle * 0.5f)
        {
            if(player[0].transform.position == lastTargetPosition)
            {
                lastTargetPosition = player[0].transform.position;
                return true;
            }

            RaycastHit hit;
            if(Physics.Raycast(transform.position, directionToPlayer, out hit,distanceToPlayer))
            {
                if(hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    bool IsInRangeAttack()
    {
        Vector3 directionToPlayer = player[0].transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if(distanceToPlayer <= attackRange && angleToPlayer < visionAngle * 0.5f)
        {
            if(player[0].transform.position == lastTargetPosition)
            {
                lastTargetPosition = player[0].transform.position;
                return true;
            }

            RaycastHit hit;
            if(Physics.Raycast(transform.position, directionToPlayer, out hit,distanceToPlayer))
            {
                if(hit.collider.CompareTag("Player"))
                {
                    
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        foreach (Transform point in patrolPoints)
        {
            Gizmos.DrawWireSphere(point.position, 1f);
        }
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Vector3  fovLine1 = Quaternion.AngleAxis(visionAngle * 0.5f, transform.up) * transform.forward * visionRange;
        Vector3  fovLine2 = Quaternion.AngleAxis(-visionAngle * 0.5f, transform.up) * transform.forward * visionRange;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    void Distracted()
    {   
        currentState = State.Patrolling;
    }
}