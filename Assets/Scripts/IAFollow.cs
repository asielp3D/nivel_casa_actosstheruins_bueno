using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAFollow : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Animator anim; 

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if(Vector3.Distance(player.position, transform.position) > 4f)
            {
            agent.SetDestination(player.position);
            }
            anim.SetFloat("VelX", 0);
            anim.SetFloat("VelZ", agent.velocity.magnitude);
        }
    }
}
